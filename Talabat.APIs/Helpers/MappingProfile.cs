using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, productToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.ProductCategory.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

        }
    }  
}
