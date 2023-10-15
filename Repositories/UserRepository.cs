using MepasTask.Abstract;
using MepasTask.Models;
using OfficeOpenXml;

namespace MepasTask.Repositories
{
    public class UserRepository:IUserRepository
    {

        private readonly IUtil util;
        public UserRepository(IUtil util)
        {
            this.util = util;   
        }

        public UserModel? findByUsername(string username)
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var userWorksheet = xlPackage.Workbook.Worksheets["User"];

                if (userWorksheet != null)
                {
                    var len = userWorksheet.Dimension.Rows;
                    int selectedIndex = 0;
                    for(int i = 2; i<=len;i++ )
                    {
                        if(userWorksheet.Cells[$"D{i}"].Text==username)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    if(selectedIndex == 0 )
                    {
                        return null;
                    }


                    string id =userWorksheet.Cells[$"A{selectedIndex}"].Text;
                    string name = userWorksheet.Cells[$"B{selectedIndex}"].Text;
                    string surname = userWorksheet.Cells[$"C{selectedIndex}"].Text;
                    string userName = userWorksheet.Cells[$"D{selectedIndex}"].Text;
                    string password = userWorksheet.Cells[$"E{selectedIndex}"].Text;
                    string status = userWorksheet.Cells[$"F{selectedIndex}"].Text;

                    UserModel model = new UserModel()
                    {
                        id = id,
                        name = name,
                        surname = surname,
                        username = userName,
                        password = password,
                        status = status,
                    };

                    return model;
                }
                else
                {
                    throw new Exception("User tablosu bulunamadı.");
                }
            }


        }


        public void addUser(UserModel user)
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (var xlPackage = new ExcelPackage(file))
            {
                var userWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "User");

                if (util.IsExcelOpen())
                {
                    var newRow = userWorksheet.Cells[userWorksheet.Dimension.End.Row + 1, 1];

                    newRow.Value = user.id;
                    newRow.Offset(0, 1).Value = user.name;   //name
                    newRow.Offset(0, 2).Value = user.surname;    //surname
                    newRow.Offset(0, 3).Value = user.username;    // username
                    newRow.Offset(0, 4).Value = user.password;  // password
                    newRow.Offset(0, 5).Value = user.status;  //status
                    xlPackage.Save();
                }
                else
                {
                    throw new Exception("Yeni bir işlem yapabilmek için lütfen exceli kapatın");
                }


            }
        }
    }
}
