using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository prdRepo;
        private readonly IMapper mapper;
        private readonly ILogService _logService;

        public ProductService (IProductRepository _prdRepo, IMapper _mapper,ILogService logService)
        {
            prdRepo = _prdRepo;
            mapper = _mapper;
            _logService = logService;
        }

        public void AddProduct(ProductDto dto)
        {
            prdRepo.Add(mapper.Map<Product>(dto));
            prdRepo.SaveChanges();
            _logService.LogAction("System",
                "Product Added",
                $"Product {dto.ProductId} Added.");
        }

        public void DeleteProduct(int id)
        {
            prdRepo.Delete(id);
            prdRepo.SaveChanges();
            _logService.LogAction("System",
                "Product Deleted",
                $"Product {id} Deleted.");
        }

        public List<ProductDto> GetAllProducts(int size = 20, int pageNumber = 1)
        {
            var prd = prdRepo.GetAll(size, pageNumber);
            return mapper.Map<List<ProductDto>>(prd);
        }

        public ProductDto? GetProductById(int id)
        {
            var prd = prdRepo.GetByID(id);
            if (prd != null)
            {
                return mapper.Map<ProductDto>(prd);
            } else
            {
                return null;
            } 
        }

        public void UpdateProduct(ProductDto dto)
        {
            prdRepo.Update(mapper.Map<Product>(dto));
            prdRepo.SaveChanges();
            _logService.LogAction("System",
                "Product Updated",
                $"Product {dto.ProductId} Updated.");
        }
    }
}
