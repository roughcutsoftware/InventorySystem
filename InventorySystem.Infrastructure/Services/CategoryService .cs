using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Repositories;

namespace InventorySystem.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository , IMapper mapper)
        {
            _categoryRepository = repository;
            _mapper = mapper;

        }
        public void AddCategory(CategoryDto dto)
        {
            _categoryRepository.Add(_mapper.Map<Category>(dto));
            _categoryRepository.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.SaveChanges() ; 
        }

        public List<CategoryDto> GetAllCategories(int size = 20, int pageNumber = 1)
        {
            var categories = _categoryRepository.GetAll(size,pageNumber);
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public CategoryDto? GetCategoryById(int id)
        {
            var categories = _categoryRepository.GetByID(id);
            if (categories == null) return null;
            return _mapper.Map<CategoryDto>(categories);
        }

        public void UpdateCategory(CategoryDto dto)
        {
            _categoryRepository.Update(_mapper.Map<Category>(dto));
            _categoryRepository.SaveChanges();
        }

    }
}
