using MepasTask.Abstract;
using MepasTask.Dto;
using MepasTask.Models;
using Microsoft.CodeAnalysis;
using OfficeOpenXml;
using System.Xml.Linq;

namespace MepasTask.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly IExcelWriteRepository excelWriteRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductRepository(IExcelWriteRepository excelWriteRepository, ICategoryRepository categoryRepository)
        {
            this.excelWriteRepository = excelWriteRepository;
            this.categoryRepository = categoryRepository;
        }

        public void AddProduct(ProductModel productModel)
        {
            var filePath = "wwwroot/Veritabani.xlsx";

            FileInfo file = new FileInfo(filePath);

            if(!excelWriteRepository.IsExcelOpen())
            {
                throw new Exception("Bu işlemin yapılabilmesi için excelin kapatılması gerekiyor");
            }

            using (var xlPackage = new ExcelPackage(file))
            {
                // "Product" sayfasını seçin
                var productWorksheet = xlPackage.Workbook.Worksheets["Product"];

                if (productWorksheet != null)
                {
                    if (excelWriteRepository.IsExcelOpen())
                    {
                        // Yeni bir satır eklemek için bir sonraki boş satırı bulun
                        int newRow = productWorksheet.Dimension?.Rows + 1 ?? 2; // 2. satırdan başlamak (ilk satır başlık olduğu için)
                        var cell = "A" + newRow.ToString();

                        productWorksheet.Cells[$"A{newRow.ToString()}"].Value = productModel.id;
                        productWorksheet.Cells[$"B{newRow.ToString()}"].Value = productModel.name;
                        productWorksheet.Cells[$"C{newRow.ToString()}"].Value = productModel.category_id;
                        productWorksheet.Cells[$"D{newRow.ToString()}"].Value = productModel.price;
                        productWorksheet.Cells[$"E{newRow.ToString()}"].Value = productModel.unit;
                        productWorksheet.Cells[$"F{newRow.ToString()}"].Value = productModel.stock;
                        productWorksheet.Cells[$"G{newRow.ToString()}"].Value = productModel.color;
                        productWorksheet.Cells[$"H{newRow.ToString()}"].Value = productModel.weight;
                        productWorksheet.Cells[$"I{newRow.ToString()}"].Value = productModel.width;
                        productWorksheet.Cells[$"J{newRow.ToString()}"].Value = productModel.height;
                        productWorksheet.Cells[$"K{newRow.ToString()}"].Value = productModel.added_user_id;
                        productWorksheet.Cells[$"L{newRow.ToString()}"].Value = productModel.updated_user_id;
                        productWorksheet.Cells[$"M{newRow.ToString()}"].Value = productModel.created_date;
                        productWorksheet.Cells[$"N{newRow.ToString()}"].Value = productModel.updated_date;
                        xlPackage.Save();
                    }
                    else
                    {
                        throw new Exception("exceli kapat");
                    }

                }
                else
                {
                    throw new Exception("Product isimli bir worksheet bulunamadı");
                }
            }
        }
    
    
        public List<ProductResponseDto> getAllProducts()
        {

            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {

                var productWorkSheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Product");
                List<ProductResponseDto> productList = new();
                if (productWorkSheet != null)
                {
                    int rowCount = productWorkSheet.Dimension.Rows;

                    // İlk satır başlık olduğu için 2. satırdan başlayarak verileri alabilirsiniz

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var prodId = productWorkSheet.Cells[$"A{row.ToString()}"].Text;
                        var name = productWorkSheet.Cells[$"B{row.ToString()}"].Text;

                        if(prodId!="" && name != "")
                        {
                            productList.Add(new ProductResponseDto()
                            {
                                id = productWorkSheet.Cells[$"A{row.ToString()}"].Text,
                                name = productWorkSheet.Cells[$"B{row.ToString()}"].Text,
                                category_id = productWorkSheet.Cells[$"C{row.ToString()}"].Text,
                                categoryName = categoryRepository.findById(productWorkSheet.Cells[$"C{row.ToString()}"].Text),
                                price = productWorkSheet.Cells[$"D{row.ToString()}"].Text,
                                unit = productWorkSheet.Cells[$"E{row.ToString()}"].Text,
                                stock = productWorkSheet.Cells[$"F{row.ToString()}"].Text,
                                color = productWorkSheet.Cells[$"G{row.ToString()}"].Text,
                                weight = productWorkSheet.Cells[$"H{row.ToString()}"].Text,
                                width = productWorkSheet.Cells[$"I{row.ToString()}"].Text,
                                height = productWorkSheet.Cells[$"J{row.ToString()}"].Text,
                                added_user_id = productWorkSheet.Cells[$"K{row.ToString()}"].Text,
                                updated_user_id = productWorkSheet.Cells[$"L{row.ToString()}"].Text,
                                created_date = productWorkSheet.Cells[$"M{row.ToString()}"].Text,
                                updated_date = productWorkSheet.Cells[$"N{row.ToString()}"].Text,
                            });
                        }

                       

                    }

                    return productList;
                }
                else
                {
                    throw new Exception("Product isimli bir worksheet bulunamadı");
                }
            }
        }


        public bool deleteProduct(string productId)
        {
            if(!excelWriteRepository.IsExcelOpen())
            {
                Console.WriteLine("excelll");
                throw new Exception("bu işlemin yapılabilmesi için excelin kapatılması gerekiyor");
            }


            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (var xlPackage = new ExcelPackage(file))
            {
                var userWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Product");

                if (userWorksheet != null)
                {
                    var len = userWorksheet.Dimension.Rows;
                    int selectedIndex = 0;
                    for (int i = 2; i <= len; i++)
                    {
                        if (userWorksheet.Cells[$"A{i}"].Text == productId)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    if (selectedIndex == 0)
                    {
                        return false;
                    }


            
                    for (int col = 1; col <= userWorksheet.Dimension.End.Column; col++)
                    {
                        userWorksheet.Cells[selectedIndex, col].Clear();
                    }

                    
                    userWorksheet.DeleteRow(selectedIndex, 1);

                  
                    xlPackage.Save();

                    return true;
                }
                else
                {
                    throw new Exception("Sayfa bulunamadı.");
                }
            }

        }

        public ProductResponseDto? findById(string productId)
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var productWorkSheet = xlPackage.Workbook.Worksheets["Product"];

                if (productWorkSheet != null)
                {
                    var len = productWorkSheet.Dimension.Rows;
                    int selectedIndex = 0;
                    for (int i = 2; i <= len; i++)
                    {
                        if (productWorkSheet.Cells[$"A{i}"].Text == productId)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    if (selectedIndex == 0)
                    {
                        return null;
                    }


                    string id = productWorkSheet.Cells[$"A{selectedIndex}"].Text;
                    string name = productWorkSheet.Cells[$"B{selectedIndex}"].Text;
                    string categoryID = productWorkSheet.Cells[$"C{selectedIndex}"].Text;
                    string categoryName = categoryRepository.findById(productWorkSheet.Cells[$"C{selectedIndex}"].Text);
                    string price = productWorkSheet.Cells[$"D{selectedIndex}"].Text;
                    string unit = productWorkSheet.Cells[$"E{selectedIndex}"].Text;
                    string stock = productWorkSheet.Cells[$"F{selectedIndex}"].Text;
                    string color = productWorkSheet.Cells[$"G{selectedIndex}"].Text;
                    string weight = productWorkSheet.Cells[$"H{selectedIndex}"].Text;
                    string width = productWorkSheet.Cells[$"I{selectedIndex}"].Text;
                    string height = productWorkSheet.Cells[$"J{selectedIndex}"].Text;
                    var categories =categoryRepository.getAllCategories();
                    ProductResponseDto responseDto = new ProductResponseDto
                    {
                        id = id,
                        name = name,
                        category_id = categoryID,
                        categoryName=categoryName,
                        price = price,
                        unit = unit,
                        stock = stock,
                        color = color,
                        weight = weight,
                        width = width,
                        height = height,
                        categories = categories
                    };
                

                    return responseDto;
                }
                else
                {
                    throw new Exception("User tablosu bulunamadı.");
                }
            }
        }


        public bool updateProduct(ProductDto productDtoModel)
        {
            if(!excelWriteRepository.IsExcelOpen())
            {

                throw new Exception("Bu işlemin yapılması için excelin kapatılması gerekli");
            }



            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var productWorkSheet = xlPackage.Workbook.Worksheets["Product"];

                if (productWorkSheet != null)
                {
                    var len = productWorkSheet.Dimension.Rows;
                    int selectedIndex = 0;
                    for (int i = 2; i <= len; i++)
                    {
                        if (productWorkSheet.Cells[$"A{i}"].Text == productDtoModel.id)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    if (selectedIndex == 0)
                    {
                        return false;
                    }
                    CategoryModel category = categoryRepository.findByName(productDtoModel.category);

                    productWorkSheet.Cells[$"B{selectedIndex}"].Value = productDtoModel.name;
                    productWorkSheet.Cells[$"C{selectedIndex}"].Value = category.id;
                    productWorkSheet.Cells[$"D{selectedIndex}"].Value = productDtoModel.price;
                    productWorkSheet.Cells[$"E{selectedIndex}"].Value = productDtoModel.unit;
                    productWorkSheet.Cells[$"F{selectedIndex}"].Value = productDtoModel.stock;
                    productWorkSheet.Cells[$"G{selectedIndex}"].Value = productDtoModel.color;
                    productWorkSheet.Cells[$"H{selectedIndex}"].Value = productDtoModel.weight;
                    productWorkSheet.Cells[$"I{selectedIndex}"].Value = productDtoModel.width;
                    productWorkSheet.Cells[$"J{selectedIndex}"].Value = productDtoModel.height;
                    productWorkSheet.Cells[$"N{selectedIndex}"].Value = DateTime.Now.ToString();
                    xlPackage.Save();

                    return true;
                }
                else
                {
                    throw new Exception("Product worksheeti bulunamadı");
                }
            }



        }

      
    }
}
