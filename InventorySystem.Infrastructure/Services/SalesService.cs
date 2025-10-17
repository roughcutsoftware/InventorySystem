
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
            sale.TotalAmount = dto.SaleDetails.Sum(d => d.Subtotal);

            sale.SaleDetails = dto.SaleDetails
                .Select(d => _mapper.Map<SaleDetails>(d)).ToList();

            foreach (var detail in sale.SaleDetails)
            {
                var product = _productRepository.GetByID(detail.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {detail.ProductId} not found");

                detail.Product = product;

                if (product.QuantityInStock < detail.Quantity)
                    throw new InvalidOperationException($"Not enough stock for {product.Name}");

                product.QuantityInStock -= detail.Quantity;
                _productRepository.Update(product);

                if (product.QuantityInStock <= product.ReorderLevel)
                    _notificationService.NotifyLowStock(product);
            }

            _salesRepository.Add(sale);
            _salesRepository.SaveChanges();
            _productRepository.SaveChanges();
        }


        public IEnumerable<Sales> GetAllSales(int size = 20, int pageNumber = 1)
        {
            return _salesRepository.GetAll(size, pageNumber);
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


