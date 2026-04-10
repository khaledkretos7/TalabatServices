using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver(IConfiguration configuration):IValueResolver<Product, productToReturnDto, string>
    {
        private readonly IConfiguration _configuration=configuration;
        public string Resolve(Product source, productToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["baseUrl"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}
