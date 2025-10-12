using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;

namespace InventorySystem.web.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Purchase, PurchaseOrderDto>().ReverseMap();
            CreateMap<PurchaseDetails, PurchaseDetailDto>().ReverseMap();

            //CreateMap<Product, ProductDto>()
            //      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            //      .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            //      .ReverseMap();
        }
    }
}
