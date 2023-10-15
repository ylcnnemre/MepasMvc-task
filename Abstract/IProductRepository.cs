using MepasTask.Dto;
using MepasTask.Models;

namespace MepasTask.Abstract
{
    public interface IProductRepository
    {
        void AddProduct(ProductModel productModel);

        List<ProductResponseDto> getAllProducts();

        bool deleteProduct(string productId);
        ProductResponseDto findById(string productId);

        bool updateProduct(ProductDto productDtoModel);
    }
}
