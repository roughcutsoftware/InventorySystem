
using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Repositories;

namespace InventorySystem.Infrastructure.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public SalesService(ISalesRepository repository, IMapper mapper, IProductRepository productRepository)
        {
            _salesRepository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }




        public void CreateSalesOrder(SalesDto dto)
        {
            var sale = _mapper.Map<Sales>(dto);
            sale.SaleDate= DateTime.Now;
            sale.Status = "Pending";
            sale.TotalAmount= dto.SaleDetails.Sum(d=>d.Subtotal);

            sale.SaleDetails = dto.SaleDetails
                .Select(d=>_mapper.Map<SaleDetails>(d)).ToList();


            foreach (var detail in sale.SaleDetails)
            {
                var product = _productRepository.GetByID(detail.ProductId);
                if (product != null)
                {
                    if (product.QuantityInStock < detail.Quantity)
                    {
                        throw new InvalidOperationException($"Not enough stock for {product.Name}");
                    }

                    product.QuantityInStock -= detail.Quantity;
                    _productRepository.Update(product);
                }
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
            var sale = _salesRepository.GetByID(id,"SalesDeatils");
            if (sale == null) return null;
            return _mapper.Map<SalesDto>(sale);
        }


        public void CancelSale(int salesId)
        {
             var sale = _salesRepository.GetByID(salesId);
            if (sale == null)  return;
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
            }
        }

    }
}
