using MepasTask.Abstract;
using MepasTask.Models;
using OfficeOpenXml;

namespace MepasTask.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly IExcelWriteRepository excelWriteRepository;

        public CategoryRepository(IExcelWriteRepository excelWriteRepository)
        {
                this.excelWriteRepository = excelWriteRepository;
        }


        public List<CategoryModel>? getAllCategories()
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {

                var categorieWorksheet = xlPackage.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Categories");
                List<CategoryModel> categoryList = new();
                if (categorieWorksheet != null)
                {
                    int rowCount = categorieWorksheet.Dimension.Rows;
                    
                    // İlk satır başlık olduğu için 2. satırdan başlayarak verileri alabilirsiniz
                    
                    for (int row = 2; row <= rowCount; row++)
                    {
                        categoryList.Add(new CategoryModel()
                        {
                           id = categorieWorksheet.Cells[$"A{row.ToString()}"].Text,
                           name = categorieWorksheet.Cells[$"B{row.ToString()}"].Text,

                    });
                            
                    }

                    return categoryList;
                }
                else
                {
                    throw new Exception("Böyle bir worksheet bulunamadı");
                }
            }

        }


        public void addCategory(CategoryModel category)
        {
            var filePath = "wwwroot/Veritabani.xlsx";

            FileInfo file = new FileInfo(filePath);

            using (var xlPackage = new ExcelPackage(file))
            {
                // "Product" sayfasını seçin
                var productWorksheet = xlPackage.Workbook.Worksheets["Categories"];

                if (productWorksheet != null)
                {
                    if (excelWriteRepository.IsExcelOpen())
                    {
                       
                        int newRow = productWorksheet.Dimension?.Rows + 1 ?? 2; 
                        var cell = "A" + newRow.ToString();

                        productWorksheet.Cells[$"A{newRow.ToString()}"].Value = category.id;
                        productWorksheet.Cells[$"B{newRow.ToString()}"].Value = category.name;
                        xlPackage.Save();
                    }
                    else
                    {
                        throw new Exception("exceli kapat");
                    }

                }
                else
                {
                    throw new Exception("Categorie isimli bir worksheet bulunamadı");
                }
            }
        }





        public CategoryModel findByName(string categoryName)
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var categoryWorkSheet = xlPackage.Workbook.Worksheets["Categories"];

                if (categoryWorkSheet != null)
                {
                    var len = categoryWorkSheet.Dimension.Rows;
                    int selectedIndex = 0;
                    for (int i = 2; i <= len; i++)
                    {
                        if (categoryWorkSheet.Cells[$"B{i}"].Text == categoryName)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    if (selectedIndex == 0)
                    {
                        return null;
                    }


                    string id = categoryWorkSheet.Cells[$"A{selectedIndex}"].Text;
                    string name = categoryWorkSheet.Cells[$"B{selectedIndex}"].Text;


                    CategoryModel category = new CategoryModel()
                    {
                        id = id,
                        name = name,
                    };

                    return category;
                }
                else
                {
                    throw new Exception("Categorie tablosu bulunamadı.");
                }
            }
        }


        public string findById(string categoryId )
        {
            var filePath = "wwwroot/Veritabani.xlsx";
            FileInfo file = new FileInfo(filePath);
            using (var xlPackage = new ExcelPackage(file))
            {
                var categoryWorkSheet = xlPackage.Workbook.Worksheets["Categories"];

                if (categoryWorkSheet != null)
                {
                    var len = categoryWorkSheet.Dimension.Rows;
                    int selectedIndex = 0;
                    for (int i = 2; i <= len; i++)
                    {
                        if (categoryWorkSheet.Cells[$"A{i}"].Text == categoryId)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    if (selectedIndex == 0)
                    {
                        return null;
                    }

                    string name = categoryWorkSheet.Cells[$"B{selectedIndex}"].Text;


                    return name;
                }
                else
                {
                    throw new Exception("Categorie tablosu bulunamadı.");
                }
            }
        }
    }
}
