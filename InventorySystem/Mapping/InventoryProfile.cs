using AutoMapper;
using InventorySystem.Core.DTOs.Inventory;
using InventorySystem.Core.Entities;

namespace InventorySystem.web.Mapping
{
    public class InventoryProfile :Profile
    {
        public InventoryProfile()
        {
            CreateMap<Product, ProductStockDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));
        }
    }
}
