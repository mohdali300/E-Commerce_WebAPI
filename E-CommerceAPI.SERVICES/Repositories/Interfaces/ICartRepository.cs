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
    public interface ICartRepository: IGenericRepository<Cart>
    {
        Task<ResponseDto> GetCart(int id);
        Task<ResponseDto> GetAllCarts();

        Task<ResponseDto> AddCart(ApplicationUser currentUser);
        //Task<ResponseDto> UpdateCart(CartDto dto);
        Task<ResponseDto> DeleteCart(int id);
    }
}
