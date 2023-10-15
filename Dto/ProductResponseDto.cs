using MepasTask.Models;

namespace MepasTask.Dto
{
    public class ProductResponseDto:ProductModel
    {
        public string? categoryName { get; set; }

        public List<CategoryModel>? categories { get; set; }

    }
}
