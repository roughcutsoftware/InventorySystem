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
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Sales, SalesDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : ""))
                .ForMember(dest => dest.SaleDetails, opt => opt.MapFrom(src => src.SaleDetails));

            CreateMap<SaleDetails, SalesDetailsDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : ""));


            //CreateMap<Product, ProductDto>()
            //      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            //      .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            //      .ReverseMap();
        }
    }
}
