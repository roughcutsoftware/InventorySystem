using InventorySystem.Core.DTOs;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories(int size = 20, int pageNumber = 1);
        CategoryDto? GetCategoryById(int id);
        void AddCategory(CategoryDto dto);
        void UpdateCategory(CategoryDto dto);
        void DeleteCategory(int id);
    }
}
