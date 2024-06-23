using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.WishlistDTO;
using E_CommerceAPI.ENTITES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        public Task<ResponseDto> GetWishlist(int id);
        public Task<ResponseDto> GetAllWishlists();

        public Task<ResponseDto> AddWishlist(WishlistDto dto,ApplicationUser user);
        public Task<ResponseDto> UpdateWishlist(int id, WishlistDto dto);
        public Task<ResponseDto> DeleteWishlist(int id);
    }
}
