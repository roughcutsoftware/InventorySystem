using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;

namespace InventorySystem.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public CategoryService(ICategoryRepository repository , IMapper mapper, ILogService logService)
        {
            _categoryRepository = repository;
            _mapper = mapper;
            _logService = logService;
        }
        public void AddCategory(CategoryDto dto)
        {
            _categoryRepository.Add(_mapper.Map<Category>(dto));
            _categoryRepository.SaveChanges();
            _logService.LogAction("System",
                "New Category Added",
                $"Category {dto.Name} Added.");
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.SaveChanges() ; 
            _logService.LogAction("System",
                "Category Deleted",
                $"Category {id} Deleted.");
        }

        //public List<CategoryDto> GetAllCategories(int size = 20, int pageNumber = 1)
        //{
        //    var categories = _categoryRepository.GetAll(size,pageNumber);
        //    return _mapper.Map<List<CategoryDto>>(categories);
        //}

        public PaginationDto<CategoryDto> GetAllCategories(int size = 20, int pageNumber = 1)
        {
            var categories = _categoryRepository.GetAll(size, pageNumber);
            var totalCount = _categoryRepository.GetTotalCount();

            return new PaginationDto<CategoryDto>
            {
                Items = _mapper.Map<List<CategoryDto>>(categories),
                PageNumber = pageNumber,
                PageSize = size,
                TotalCount = totalCount
            };
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
            _logService.LogAction("System",
                "Category Data Updated",
                $"Category {dto.Name} Updated.");
        }
    }
}
