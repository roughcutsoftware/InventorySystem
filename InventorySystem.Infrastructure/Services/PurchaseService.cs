
using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;

namespace InventorySystem.Infrastructure.Services
{
    public class PurchaseService(IPurchaseRepository repository, IProductRepository productRepository, IMapper mapper,
        ILogService _logService) : IPurchaseService
    {

        private readonly IPurchaseRepository _purchaseRepository = repository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public void CancelPurchase(int id)
        {
            var purchaseItem = _purchaseRepository.GetByID(id);
            if (purchaseItem == null)
                return;
            purchaseItem.Status = "Cancelled";
            _purchaseRepository.Update(purchaseItem);
            _purchaseRepository.SaveChanges();
        }

        public void CreatePurchaseOrder(PurchaseOrderDto dto)
        {
            var purchase = _mapper.Map<Purchase>(dto);

            purchase.PurchaseDate = DateTime.Now;
            purchase.Status = "Pending";
            purchase.TotalAmount = dto.PurchaseDetails.Sum(d => d.subTotal);

            purchase.PurchaseDetails = dto.PurchaseDetails
                .Select(d => _mapper.Map<PurchaseDetails>(d))
                .ToList();

            _purchaseRepository.Add(purchase);
            _purchaseRepository.SaveChanges();

            _logService.LogAction("System",
                "Create Purchase Order",
                $"Purchase {dto.PurchaseId} created.");
        }

        public IEnumerable<Purchase> GetAllPurchases(int size = 20, int pageNumber = 1)
        {
            return _purchaseRepository.GetAll(size, pageNumber);
        }

        public PurchaseOrderDto? GetPurchaseById(int id)
        {
            var purchase = _purchaseRepository.GetByID(id);
            if(purchase == null) 
               return null;
            
            return _mapper.Map<PurchaseOrderDto>(purchase);
        }

        public void ReceiveStock(int purchaseId)
        {
            var purchase = _purchaseRepository.GetByID(purchaseId, "PurchaseDetails");
            if (purchase == null)
                return;

            foreach (var detail in purchase.PurchaseDetails)
            {
                var product = _productRepository.GetByID(detail.ProductId);
                if (product != null)
                {
                    product.QuantityInStock += detail.Quantity;
                    _productRepository.Update(product);
                }
            }

            purchase.Status = "Received";
            _purchaseRepository.Update(purchase);
            _purchaseRepository.SaveChanges();
            _productRepository.SaveChanges();

            _logService.LogAction("System",
                "New purchase Stock Added",
                $"purchase {purchaseId} Added.");
        }
    }
}
