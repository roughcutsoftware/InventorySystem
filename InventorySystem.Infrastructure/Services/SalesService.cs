
using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Repositories;

namespace InventorySystem.Infrastructure.Services
{
    public class SalesService :ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        public SalesService(ISalesRepository repository, IMapper mapper)
        {
            _salesRepository = repository;
            _mapper = mapper;
        }


      

        public void CreateSalesOrder(SalesDto dto)
        {
            var sale = _mapper.Map<Sales>(dto);
            sale.SaleDate= DateTime.Now;
            sale.Status = "Pending";
            sale.TotalAmount= dto.SaleDetails.Sum(d=>d.Subtotal);

            sale.SaleDetails = dto.SaleDetails
                .Select(d=>_mapper.Map<SaleDetails>(d)).ToList();
            _salesRepository.Add(sale);
            _salesRepository.SaveChanges();

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


        public void CancelSales(int salesId)
        {
             var sale = _salesRepository.GetByID(salesId);
            if (sale == null)  return;
            sale.Status = "Cancelled";
            _salesRepository.Update(sale);
            _salesRepository.SaveChanges();
        }




        //public void ReceiveStock(int purchaseId)
        //{
        //    var purchase = _purchaseRepository.GetByID(purchaseId, "PurchaseDetails");
        //    if (purchase == null)
        //        return;

        //    //foreach (var detail in purchase.PurchaseDetails)
        //    //{
        //    //    var product = _productRepository.GetByID(detail.ProductId);
        //    //    if (product != null)
        //    //    {
        //    //        product.QuantityInStock += detail.Quantity;
        //    //        _productRepository.Update(product);
        //    //    }
        //    //}

        //    purchase.Status = "Received";
        //    _purchaseRepository.Update(purchase);
        //    _purchaseRepository.SaveChanges();
        //    //_productRepository.SaveChanges();
        //}


        public void ReduceStock(int salesId,int quantity)
        {

        }













    }
}
