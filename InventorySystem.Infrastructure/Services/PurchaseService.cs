
using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Repositories;

namespace InventorySystem.Infrastructure.Services
{
    public class PurchaseService(IPurchaseRepository repository, IProductRepository productRepository, IMapper mapper,
        ILogService _logService) : IPurchaseService
    {

        private readonly IPurchaseRepository _purchaseRepository = repository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogService _logService = _logService;

        public void CancelPurchase(int id)
        {
            var purchaseItem = _purchaseRepository.GetByID(id);
            if (purchaseItem == null)
            {
                _logService.LogAction("System", "Cancel Purchase", $"Attempted to cancel non-existing Purchase #{id}");
                return;
            }

            purchaseItem.Status = "Cancelled";
            _purchaseRepository.Update(purchaseItem);
            _purchaseRepository.SaveChanges();

            _logService.LogAction("System", "Cancel Purchase", $"Purchase #{id} cancelled.");
        }


        public void CreatePurchaseOrder(PurchaseOrderDto dto)
        {
            var purchase = new Purchase
            {
                PurchaseDate = DateTime.Now,
                Status = "Pending",
                SupplierId = dto.SupplierId,
                CreatedById = dto.CreatedById,
                PurchaseDetails = new List<PurchaseDetails>()
            };

            foreach (var item in dto.PurchaseDetails)
            {
                var product = _productRepository.GetByID(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

                var unitCost = item.UnitCost > 0 ? item.UnitCost : product.UnitPrice;
                var subtotal = item.Quantity * unitCost;

                purchase.PurchaseDetails.Add(new PurchaseDetails
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitCost = unitCost,
                    subTotal = subtotal
                });
            }

            purchase.TotalAmount = purchase.PurchaseDetails.Sum(d => d.subTotal);

            _purchaseRepository.Add(purchase);
            _purchaseRepository.SaveChanges();

            _logService.LogAction("System", "Create Purchase Order", $"Purchase #{purchase.PurchaseId} created.");
        }

        //public IEnumerable<Purchase> GetAllPurchases(int size = 20, int pageNumber = 1)
        //{
        //    return _purchaseRepository.GetAll(size, pageNumber).OrderByDescending(p=>p.PurchaseDate);
        //}

        public PaginationDto<Purchase> GetAllPurchases(int size = 20, int pageNumber = 1)
        {
            var purchases = _purchaseRepository.GetAll(size, pageNumber);
            var totalCount = _purchaseRepository.GetTotalCount();

            return new PaginationDto<Purchase>
            {
                Items = _mapper.Map<List<Purchase>>(purchases),
                PageNumber = pageNumber,
                PageSize = size,
                TotalCount = totalCount
            };
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

            if (purchase.Status == "Received")
            {
                _logService.LogAction("System", "Receive Stock", $"Purchase #{purchaseId} already received.");
                return;
            }

            foreach (var detail in purchase.PurchaseDetails)
            {
                var product = _productRepository.GetByID(detail.ProductId.Value);
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
