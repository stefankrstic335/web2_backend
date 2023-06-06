using AutoMapper;
using Backend.Dto;
using Backend.Models;

namespace Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<User, AccountDataDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }

    }
}
