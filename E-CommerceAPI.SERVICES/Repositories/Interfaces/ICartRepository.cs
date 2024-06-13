using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<ResponseDto> GetCart(int id);
        Task<ResponseDto> GetAllCarts();

        Task<ResponseDto> AddCart(ApplicationUser currentUser);
        //Task<ResponseDto> UpdateCart(CartDto dto);
        Task<ResponseDto> DeleteCart(int id);
    }
}
