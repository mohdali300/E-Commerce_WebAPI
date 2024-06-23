using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.WishlistDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class WishlistRepository:GenericRepository<Wishlist>,IWishlistRepository
    {
        private readonly IMapper _mapper;

        public WishlistRepository(ECommerceDbContext context,IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetWishlist(int id)
        {
            var wishlist=await _context.Wishlists.FindAsync(id);
            if(wishlist != null)
            {
                var dto=_mapper.Map<WishlistDto>(wishlist);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = true,
                Message="Wishlist not found."
            };
        }

        public async Task<ResponseDto> GetAllWishlists()
        {
            var lists=await _context.Wishlists.AsNoTracking().ToListAsync();
            if(lists != null && lists.Count>0)
            {
                var dto=_mapper.Map<List<WishlistDto>>(lists);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = true,
                Message = "There is no wishlists."
            };
        }

        public async Task<ResponseDto> AddWishlist(WishlistDto dto,ApplicationUser user)
        {
            var list = _mapper.Map<Wishlist>(dto);
            list.CustomerId = user.Id;
            list.Customer=user;

            await _context.AddAsync(list);
            var entity=_context.Entry(list);
            if(entity.State==EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message="Failed to add wishlist."
            };
        }

        public async Task<ResponseDto> UpdateWishlist(int id, WishlistDto dto)
        {
            var list = await _context.Wishlists.FindAsync(id);
            if(list == null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = true,
                    Message = "There is no wishlists."
                };
            }

            var wish = _mapper.Map<Wishlist>(dto);
            wish.Id = id;
            wish.Customer = list.Customer;
            wish.CustomerId=list.CustomerId;
            wish.wishlistItems = list.wishlistItems;

            _context.Entry(list).CurrentValues.SetValues(wish);
            var entity = _context.Entry(list);
            if (entity.State == EntityState.Modified)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to edit wishlist."
            };
        }

        public async Task<ResponseDto> DeleteWishlist(int id)
        {
            var list =await _context.Wishlists.FindAsync(id);
            if (list==null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = true,
                    Message = "Wishlist not found."
                };
            }

            _context.Remove(list);
            var entity = _context.Entry(list);
            if (entity.State == EntityState.Deleted)
            {
                var dto = _mapper.Map<WishlistDto>(list);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to delete wishlist."
            };
        }

    }
}
