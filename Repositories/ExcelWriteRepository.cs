using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MepasTask.Abstract;
using MepasTask.Models;

namespace MepasTask.Repositories
{
    public class ExcelWriteRepository: IExcelWriteRepository
    {
        public bool IsExcelOpen()  // Excelin  açık olup olmadığını kontrol eden metod
        {
            Process[] processes = Process.GetProcessesByName("EXCEL");

            if(processes.Length == 0 )
            {
                return true;
            }
            return false;
            
        }

        public void CreateProductsWorkSheet()   // product worksheeti oluşturan metod
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var productWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Product");

                if (productWorksheet == null)
                {
                    productWorksheet = xlPackage.Workbook.Worksheets.Add("Product");

                    using (var r = productWorksheet.Cells["A1:N1"])
                    {
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    // "Product" sayfasına başlık satırlarını ekle
                    productWorksheet.Cells["A1"].Value = "id";
                    productWorksheet.Cells["B1"].Value = "name";
                    productWorksheet.Cells["C1"].Value = "category_id";
                    productWorksheet.Cells["D1"].Value = "price";
                    productWorksheet.Cells["E1"].Value = "unit";
                    productWorksheet.Cells["F1"].Value = "stock";
                    productWorksheet.Cells["G1"].Value = "color";
                    productWorksheet.Cells["H1"].Value = "weight";
                    productWorksheet.Cells["I1"].Value = "width";
                    productWorksheet.Cells["J1"].Value = "height";
                    productWorksheet.Cells["K1"].Value = "added_user_id";
                    productWorksheet.Cells["L1"].Value = "updated_user_id";
                    productWorksheet.Cells["M1"].Value = "created_date";
                    productWorksheet.Cells["N1"].Value = "updated_date";

                    productWorksheet.Cells.AutoFitColumns();
                }

                xlPackage.Save();
            }


        }

        public void CreateUsersWorkSheet()
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var userWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "User");

                if (userWorksheet == null)
                {
                    userWorksheet = xlPackage.Workbook.Worksheets.Add("User");
                    using (var r = userWorksheet.Cells["A1:F1"])
                    {
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    userWorksheet.Cells["A1"].Value = "id";
                    userWorksheet.Cells["B1"].Value = "name";
                    userWorksheet.Cells["C1"].Value = "surname";
                    userWorksheet.Cells["D1"].Value = "username";
                    userWorksheet.Cells["E1"].Value = "password";
                    userWorksheet.Cells["F1"].Value = "status";
                    userWorksheet.Cells.AutoFitColumns();
                }

                xlPackage.Save();
            }
        }   // user worksheetini oluşturan metod


        public void CreateCategoryWorkSheet()   // kategori worksheetini oluşturan metod
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var userWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Categories");
                
                if (userWorksheet == null)
                {
                    userWorksheet = xlPackage.Workbook.Worksheets.Add("Categories");
                    using (var r = userWorksheet.Cells["A1:B1"])
                    {
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    userWorksheet.Cells["A1"].Value = "id";
                    userWorksheet.Cells["B1"].Value = "name";


                    userWorksheet.Cells.AutoFitColumns();
                }

                xlPackage.Save();
            }
        }


        
    }
}
