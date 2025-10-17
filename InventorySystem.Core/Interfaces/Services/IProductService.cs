using InventorySystem.Core.DTOs;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts(int size = 20, int pageNumber = 1);
        ProductDto? GetProductById(int id);
        void AddProduct(ProductDto dto);
        void UpdateProduct(ProductDto dto);
        void DeleteProduct(int id);

    }
}
