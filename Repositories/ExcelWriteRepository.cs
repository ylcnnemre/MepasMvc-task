﻿using OfficeOpenXml.Style;
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
        public bool IsExcelOpen()
        {
            Process[] processes = Process.GetProcessesByName("EXCEL");

            if(processes.Length == 0 )
            {
                return true;
            }
            return false;
            
        }

        public void CreateProductsWorkSheet()
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                // "Product" sayfasının mevcut olup olmadığını kontrol edin
                var productWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Product");

                if (productWorksheet == null)
                {
                    // "Product" sayfası yoksa oluşturun
                    productWorksheet = xlPackage.Workbook.Worksheets.Add("Product");

                    using (var r = productWorksheet.Cells["A1:N1"])
                    {
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    // "Product" sayfasına başlık satırlarını ekleyin
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
        }


        public void CreateCategoryWorkSheet()
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                // "User" sayfasının mevcut olup olmadığını kontrol edin
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

                    // "User" sayfası yoksa oluşturun
                

                    // "User" sayfasına başlık satırlarını ekleyin
                    userWorksheet.Cells["A1"].Value = "id";
                    userWorksheet.Cells["B1"].Value = "name";


                    userWorksheet.Cells.AutoFitColumns();
                }

                xlPackage.Save();
            }
        }


        public void AddProduct()
        {
            var filePath = "wwwroot/Veritabani.xlsx";

            FileInfo file = new FileInfo(filePath);

            using (var xlPackage = new ExcelPackage(file))
            {
                // "Product" sayfasını seçin
                var productWorksheet = xlPackage.Workbook.Worksheets["Product"];
                
                if (productWorksheet != null)
                {
                    if(IsExcelOpen())
                    {
                        // Yeni bir satır eklemek için bir sonraki boş satırı bulun
                        int newRow = productWorksheet.Dimension?.Rows + 1 ?? 2; // 2. satırdan başlamak (ilk satır başlık olduğu için)
                        var cell = "A" + newRow.ToString();
                        
                        productWorksheet.Cells[cell].Value = "est";

                        xlPackage.Save();
                    }
                    else
                    {
                        throw new Exception("exceli kapat");
                    }
                   
                }
                else
                {
                    throw new Exception("Eklerken bir hata oluştu");
                }
            }
        }

        
    }
}
