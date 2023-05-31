using AutoMapper;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Images.Queries.Vms;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>();
            CreateMap<Review, ReviewVm>();
            CreateMap<Image, ImageVm>();
            CreateMap<Address, AddressVm>();
        }
    }
}