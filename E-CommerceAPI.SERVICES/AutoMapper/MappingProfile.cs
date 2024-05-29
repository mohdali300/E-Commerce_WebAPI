using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.UserDTO;
using E_CommerceAPI.ENTITES.Models;

namespace E_CommerceAPI.SERVICES.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto,ApplicationUser>().ReverseMap();
            CreateMap<UserDto,ApplicationUser>().ReverseMap();
            CreateMap<Product,ProductDto>()
                .ForMember(dest=>dest.Brand,src=>src.MapFrom(src=>src.Brand!.Name))
                .ForMember(dest => dest.Category, src => src.MapFrom(src => src.Category!.Name))
                .ReverseMap();

        }
    }
}
