using AutoMapper;

namespace Carl_Assignment.Entity
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
        }
    }
}
