using E_CommerceAPI.ENTITES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<ResponseDto> GetOrderById(int id);
        public Task<ResponseDto> GetCustomerOrders();
        public Task<ResponseDto> GetOrderItems(int id);
        public Task<ResponseDto> GetAllOrdersItems();

    }
}
