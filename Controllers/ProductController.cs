using MepasTask.Abstract;
using MepasTask.Dto;
using MepasTask.Models;
using MepasTask.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MepasTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly IExcelWriteRepository _excelWriteRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository categoryRepository;
        public ProductController(IExcelWriteRepository excelWriteRepository, IProductRepository productRepository,IUserRepository userRepository,ICategoryRepository categoryRepository)
        {
            this._excelWriteRepository = excelWriteRepository;
            this._productRepository = productRepository;
            this.userRepository=userRepository;
            this.categoryRepository=categoryRepository;
        }


        [Authorize]
        public IActionResult Index()
        {
            try
            {
                List<ProductResponseDto> prod = _productRepository.getAllProducts();
                return View(prod);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
         
        }

    
        [HttpGet("/test")]
        public IActionResult test()
        {
            return Ok(new
            {
                isim = "emre",
                soyisim = "yalcin"
            });
        }


        [HttpGet("[action]")]
        public IActionResult AddProduct()
        {
            try
            {
                List<CategoryModel> categoryModel = categoryRepository.getAllCategories();

                return View(categoryModel);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
           
        }

        [HttpPost("[action]")]
        public IActionResult AddProduct([FromBody] ProductDto product)
        {
            try
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                UserModel result = userRepository.findByUsername(username);
                CategoryModel category = categoryRepository.findByName(product.category);

                ProductModel productModel = new ProductModel()
                {
                    id = Guid.NewGuid().ToString(),
                    name = product.name,
                    color = product.color,
                    height = product.height,
                    width = product.width,
                    price = product.price,
                    unit = product.unit,
                    stock = product.stock,
                    weight = product.weight,
                    created_date = DateTime.Now.ToString(),
                    updated_date = DateTime.Now.ToString(),
                    added_user_id = result.id,
                    updated_user_id = result.id,
                    category_id = category.id

                };

                _productRepository.AddProduct(productModel);


                return Ok(new
                {
                    msg = "kayıt işlemi başarılı"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    msg = ex.Message,   
                });
            }
          
        }



        [HttpDelete("[action]/{id}")]
        public IActionResult deleteProduct([FromRoute] string id )
        {
            try
            {
                var result = _productRepository.deleteProduct(id);

                if(result)
                {
                    return Ok(new
                    {
                        msg = "silme işlemi başarılı"
                    });
                }

                return NotFound(new
                {
                    msg = "Belirtilen kayıt bulunamadı"
                });


            }
            catch(Exception ex)
            {
                return NotFound(new
                {
                    msg = ex.Message,
                });
            }
        }



        [HttpGet("[action]/{id}")]
        public IActionResult editProduct([FromRoute] string id )
        {
            try
            {
                ProductResponseDto responseDto = _productRepository.findById(id);

                
                return View(responseDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult updateProduct([FromBody] ProductDto product)
        {
            try
            {
              var result = _productRepository.updateProduct(product);


              if(result)
                {
                    return Ok(new
                    {
                        msg= "Güncelleme işlemi başarılı"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        msg = "Aranan kayıt bulunamadı"
                    });
                }

            }
            catch(Exception ex )
            {
                return NotFound(new
                {
                    msg = ex.Message
                });
            }
        }



        [HttpGet("/addv")]
        [AllowAnonymous]
        public IActionResult add()
        {
            try
            {
                string uid = Guid.NewGuid().ToString();


                return Ok(new
                {
                    id = uid,
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }

        
        
    }
}
