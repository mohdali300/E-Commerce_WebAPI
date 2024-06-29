using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.CartDTO;
using E_CommerceAPI.ENTITES.DTOs.OrderDTO;
using E_CommerceAPI.ENTITES.DTOs.PaymentDTO;
using E_CommerceAPI.ENTITES.DTOs.ProductDTO;
using E_CommerceAPI.ENTITES.DTOs.UserDTO;
using E_CommerceAPI.ENTITES.DTOs.WishlistDTO;
using E_CommerceAPI.ENTITES.Models;

namespace E_CommerceAPI.SERVICES.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto,ApplicationUser>().ReverseMap();
            CreateMap<UserDto,ApplicationUser>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Brand, src => src.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.Category, src => src.MapFrom(src => src.Category!.Name))
                .ReverseMap();
            CreateMap<Product, AddProductDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItems, OrderItemDto>()
                .ForMember(dest=>dest.ProductName,src=>src.MapFrom(src=>src.Product!.Name))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Brand,BrandDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItems, CartItemsDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Wishlist, WishlistDto>().ReverseMap();
            CreateMap<WishlistItems, WishlistItemsDto>()
                .ForMember(dest => dest.WishlistName, src => src.MapFrom(src => src.Wishlist!.Name))
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product!.Name))
                .ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
