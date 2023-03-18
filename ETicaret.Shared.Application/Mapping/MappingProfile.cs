using AutoMapper;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Dal.Concrete;

namespace ETicaret.Shared.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product,ProductDto >();
            CreateMap<Category,CategoryDTO>();

        }
    }
}
