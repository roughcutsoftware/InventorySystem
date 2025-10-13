using InventorySystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventorySystem.web.View_Models
{
    public class Product_ViewModel
    {
        public ProductDto Product { get; set; } = new ProductDto();
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<SupplierDto> Suppliers { get; set; } = new List<SupplierDto>();
    }
}
