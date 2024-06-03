using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.OrderDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;

        public OrderRepository(ECommerceDbContext context, IMapper mapper) :base(context)
        {
            _mapper = mapper;
        }


        public async Task<ResponseDto> GetOrderById(int id)
        {
            var order=await _context.Orders.FindAsync(id);
            
            if (order == null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    Message = "Order Not Found!",
                    IsSucceeded = false
                };
            }

            var dto=_mapper.Map<OrderDto>(order);
            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model = dto
            };
        }

        public async Task<ResponseDto> GetCustomerOrders()
        {
            var orders=await _context.Orders.AsNoTracking().ToListAsync();
            if(orders!=null && orders.Count > 0)
            {
                var dto=_mapper.Map<List<OrderDto>>(orders);

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
                Message = "There is no Orders."
            };
        }

        public async Task<ResponseDto> GetOrderItems(int id)
        {
            var order = await _context.OrderItems.Where(o => o.Id == id)
                .Include(o => o.Product).FirstOrDefaultAsync();

            if (order != null)
            {
                var dto = _mapper.Map<OrderItemDto>(order);

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                IsSucceeded = false,
                StatusCode = 400,
                Message = "Items not Founded!"
            };
        }

        public async Task<ResponseDto> GetAllOrdersItems()
        {
            var orders = await _context.OrderItems.AsNoTracking()
                .Include(o => o.Product).ToListAsync();

            if (orders != null && orders.Count>0)
            {
                var dto = _mapper.Map<List<OrderItemDto>>(orders);

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                IsSucceeded = false,
                StatusCode = 400,
                Message = "There is no Items yet!"
            };
        }


    }
}
