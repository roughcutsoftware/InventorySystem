using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Repositories;

namespace InventorySystem.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository repository, IMapper mapper) {
            _supplierRepository = repository;
            _mapper = mapper;
        }

        public void AddSupplier(SupplierDto dto)
        {
            _supplierRepository.Add(_mapper.Map<Supplier>(dto));
            _supplierRepository.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            _supplierRepository.Delete(id);
            _supplierRepository.SaveChanges();
        }

        public List<SupplierDto> GetAllSuppliers(int size = 20, int pageNumber = 1)
        {
            var supplires = _supplierRepository.GetAll(size, pageNumber);
            return _mapper.Map<List<SupplierDto>>(supplires);
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
        }
    }
}
