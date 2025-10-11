using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;

namespace InventorySystem.web.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Purchase, PurchaseOrderDto>().ReverseMap();
            CreateMap<PurchaseDetails, PurchaseDetailDto>().ReverseMap();
        }
    }
}
