using InventorySystem.Core.DTOs;

namespace InventorySystem.web.View_Models
{
    public class Category_Details_ViewModel
    {
        public CategoryDto Category { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }
}
