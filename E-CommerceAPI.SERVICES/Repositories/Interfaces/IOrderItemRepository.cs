using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<ResponseDto> GetOrderItems(int id);
        public Task<ResponseDto> GetAllItems();
        public Task<ResponseDto> GetItemsInOrder(int orderId);

        public Task<ResponseDto> AddOrderItem(OrderItems item);

        public Task<ResponseDto> UpdateOrderItem(int id, OrderItems item);

        public Task<ResponseDto> DeleteOrderItem(int id);
    }
}
