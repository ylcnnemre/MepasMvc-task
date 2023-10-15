using MepasTask.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.IO.Abstractions;
using MepasTask.Abstract;
using Microsoft.AspNetCore.Http;
using MepasTask.Dto;
using MepasTask.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MepasTask.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IExcelWriteRepository excelWriteRepository;

        public LoginController(IUserRepository userRepository, IExcelWriteRepository excelWriteRepository)
        {
            this._userRepository = userRepository;
            this.excelWriteRepository= excelWriteRepository;
 
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {

            excelWriteRepository.CreateAllWorkSheets();
         
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromBody] LoginUserDto user)
        {
            var result =_userRepository.findByUsername(user.username);

            if(result==null || result.password != user.password )
            {
                return NotFound(new
                {
                    msg = "Kullanıcı adı veya parola yanlis"
                });
            }
            else
            {
             
                return Ok(new
                {
                    msg = "Giriş işlemi başarılı"
                });

            }



        }

        [HttpPost("confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> loginConfirm([FromBody] LoginUserDto user)
        {
            if(user.code== 1111)
            {
                var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name,user.username),
             
                       };

                   var userIdentity = new ClaimsIdentity(claims, "a");
                   ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                   await HttpContext.SignInAsync(claimsPrincipal);


                return Ok(new
                {
                    msg = "Onay kodu doğru"
                });
            }
            return BadRequest(new
            {
                msg="Onay kodu Yanlış"
            });


        }


        [HttpGet("[action]")]

        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Product");
        }
    }
}
