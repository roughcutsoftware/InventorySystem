
using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;

namespace InventorySystem.Infrastructure.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;

        public SalesService(ISalesRepository repository, IMapper mapper, IProductRepository productRepository, INotificationService notificationService)
        {
            _salesRepository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
            _notificationService = notificationService;
        }
        public void CreateSalesOrder(SalesDto dto)
        {
            var sale = _mapper.Map<Sales>(dto);
            sale.SaleDate = DateTime.Now;
            sale.Status = "Pending";
            sale.SaleDetails = new List<SaleDetails>();

            decimal total = 0;

            foreach (var detailDto in dto.SaleDetails)
            {
                var product = _productRepository.GetByID(detailDto.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {detailDto.ProductId} not found.");

                var unitPrice = product.UnitPrice;
                var subtotal = unitPrice * detailDto.Quantity;

                if (product.QuantityInStock < detailDto.Quantity)
                    throw new InvalidOperationException($"Not enough stock for {product.Name}.");

                product.QuantityInStock -= detailDto.Quantity;
                _productRepository.Update(product);

                if (product.QuantityInStock <= product.ReorderLevel)
                    _notificationService.NotifyLowStock(product);

                var saleDetail = new SaleDetails
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = unitPrice,
                    Subtotal = subtotal
                };

                sale.SaleDetails.Add(saleDetail);
                total += subtotal;
            }

            sale.TotalAmount = total;

            _salesRepository.Add(sale);
            _salesRepository.SaveChanges();
            _productRepository.SaveChanges();
        }
        public IEnumerable<Sales> GetAllSales(int size = 20, int pageNumber = 1)
        {
            return _salesRepository.GetAll(size, pageNumber).OrderByDescending(p=>p.SaleDate);
        }
        public SalesDto? GetSalesById(int id)
        {
            var sale = _salesRepository.GetByID(id, "SaleDetails");
            if (sale == null) return null;
            return _mapper.Map<SalesDto>(sale);
        }

        public void CancelSale(int salesId)
        {
            var sale = _salesRepository.GetByID(salesId);
            if (sale == null) return;
            sale.Status = "Cancelled";


            foreach (var detail in sale.SaleDetails)
            {
                var product = _productRepository.GetByID(detail.ProductId);
                if (product != null)
                {
                    product.QuantityInStock += detail.Quantity;
                    _productRepository.Update(product);
                }
            }

            _salesRepository.Update(sale);
            _salesRepository.SaveChanges();
            _productRepository.SaveChanges();
        }

        public void ReduceStock(int productId, int quantity)
        {
            var product = _productRepository.GetByID(productId);
            if (product != null)
            {
                product.QuantityInStock -= quantity;
                _productRepository.Update(product);
                _productRepository.SaveChanges();
                if (product.QuantityInStock <= product.ReorderLevel)
                {
                    _notificationService.NotifyLowStock(product);
                }

            }
        }
        public void MarkAsCompleted(int id)
        {
            var sale = _salesRepository.GetByID(id);
            if (sale == null)
                throw new Exception("Sale not found");

            sale.Status = "Completed";
            _salesRepository.Update(sale);
            _salesRepository.SaveChanges();
        }


    }
}