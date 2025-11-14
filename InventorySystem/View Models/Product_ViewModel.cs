using InventorySystem.Core.DTOs;

namespace InventorySystem.Web.View_Models
{
    public class Product_ViewModel
    {
        public ProductDto Product { get; set; } = new ProductDto();
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<SupplierDto> Suppliers { get; set; } = new List<SupplierDto>();
    }
}
