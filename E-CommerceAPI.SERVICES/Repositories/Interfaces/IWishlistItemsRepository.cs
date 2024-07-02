using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IWishlistItemsRepository: IGenericRepository<WishlistItems>
    {
        public Task<ResponseDto> GetWishlistItem(int id);
        public Task<ResponseDto> GetAllWishlistItems();
        public Task<ResponseDto> GetItemsInWishlist(int listId);

        public Task<ResponseDto> AddWishlistItem(WishlistItems item);
        public Task<ResponseDto> UpdateWishlistItem(int id, WishlistItems item);
        public Task<ResponseDto> DeleteWishlistItem(int id);
    }
}
