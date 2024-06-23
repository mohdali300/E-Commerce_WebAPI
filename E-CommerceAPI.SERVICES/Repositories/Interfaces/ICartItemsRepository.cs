using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.CartDTO;
using E_CommerceAPI.ENTITES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface ICartItemsRepository
    {
        Task<ResponseDto> GetAllCartsItems();
        public Task<ResponseDto> GetCartItem(int id);
        Task<ResponseDto> GetItemsInCart(int cartId);

        Task<ResponseDto> AddItemToCart(CartItems item);
        Task<ResponseDto> UpdateCartItem(int id,CartItems item);
        Task<ResponseDto> DeleteItemFromCart(int id);

    }
}
