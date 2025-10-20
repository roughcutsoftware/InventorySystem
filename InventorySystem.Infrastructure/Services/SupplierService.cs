using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Repositories;

namespace InventorySystem.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public SupplierService(ISupplierRepository repository, IMapper mapper, ILogService logService) {
            _supplierRepository = repository;
            _mapper = mapper;
            _logService = logService;
        }

        public void AddSupplier(SupplierDto dto)
        {
            _supplierRepository.Add(_mapper.Map<Supplier>(dto));
            _supplierRepository.SaveChanges();
            _logService.LogAction("System",
                "New Supplier Added",
                $"Supplier {dto.Name} Added.");
        }

        public void DeleteSupplier(int id)
        {
            _supplierRepository.Delete(id);
            _supplierRepository.SaveChanges();
            _logService.LogAction("System",
                "Supplier Deleted",
                $"Supplier {id} Deleted.");
        }

        //public List<SupplierDto> GetAllSuppliers(int size = 20, int pageNumber = 1)
        //{
        //    var supplires = _supplierRepository.GetAll(size, pageNumber);
        //    return _mapper.Map<List<SupplierDto>>(supplires);
        //}

        public PaginationDto<SupplierDto> GetAllSuppliers(int size = 20, int pageNumber = 1)
        {
            var suppliers = _supplierRepository.GetAll(size, pageNumber);
            var totalCount = _supplierRepository.GetTotalCount();

            return new PaginationDto<SupplierDto>
            {
                Items = _mapper.Map<List<SupplierDto>>(suppliers),
                PageNumber = pageNumber,
                PageSize = size,
                TotalCount = totalCount
            };
        }

        public SupplierDto? GetSupplierById(int id)
        {
            var supplier = _supplierRepository.GetByID(id);
            if (supplier == null)
                return null;
            return _mapper.Map<SupplierDto>(supplier);
        }

        public void UpdateSupplier(SupplierDto dto)
        {
            _supplierRepository.Update(_mapper.Map<Supplier>(dto));
            _supplierRepository.SaveChanges();
            _logService.LogAction("System",
                "Supplier Data Updated",
                $"Supplier {dto.Name} Updated.");
        }
    }
}
