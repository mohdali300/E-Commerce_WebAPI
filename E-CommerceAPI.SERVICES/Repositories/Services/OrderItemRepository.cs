using E_CommerceAPI.ENTITES.DTOs.OrderDTO;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceAPI.SERVICES.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class OrderItemRepository:GenericRepository<OrderItems>,IOrderItemRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderItemRepository(ECommerceDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager) : base(context)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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

        public async Task<ResponseDto> GetAllItems()
        {
            var orders = await _context.OrderItems.AsNoTracking()
                .Include(o => o.Product).ToListAsync();

            if (orders != null && orders.Count > 0)
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

        public async Task<ResponseDto> GetItemsInOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.orderItems = await _context.OrderItems.Where(o => o.OrderId == orderId)
                    .Include(o => o.Product).ToListAsync();
                if (order.orderItems != null && order.orderItems.Count > 0)
                {
                    var dto = _mapper.Map<List<OrderItemDto>>(order.orderItems);
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
                    Message = "There is no items yet in this order."
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This order is not exist."
            };
        }


        public async Task<ResponseDto> AddOrderItem(OrderItems item)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == item.ProductId))
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    StatusCode = 400,
                    Message = "The Product item you try to add is not exist."
                };
            }

            if (!await _context.Orders.AnyAsync(o => o.Id == item.OrderId))
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    StatusCode = 400,
                    Message = "The Order you try to add this item on is not exist."
                };
            }

            item.Order = await _context.Orders.FindAsync(item.OrderId);
            item.Product = await _context.Products.FindAsync(item.ProductId);
            if (item.Product != null)
                item.TotalPrice = item.Quantity * item.Product.Price;

            await _context.OrderItems.AddAsync(item);
            var entity = _context.Entry(item);
            if (entity.State == EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = new
                    {
                        Item = item,
                        Product = await _context.Products.Where(p => p.Id == item.ProductId)
                        .Select(p => p.Name).FirstOrDefaultAsync(),
                    }
                };
            }

            return new ResponseDto
            {
                IsSucceeded = false,
                StatusCode = 400,
                Message = "Failed to add this item,try again."
            };
        }

        public async Task<ResponseDto> UpdateOrderItem(int id, OrderItems item)
        {
            var result = await _context.OrderItems.FindAsync(id);
            if (result != null)
            {
                if (!await _context.Products.AnyAsync(p => p.Id == item.ProductId))
                {
                    return new ResponseDto
                    {
                        IsSucceeded = false,
                        StatusCode = 400,
                        Message = "The Product item you try to add is not exist."
                    };
                }

                if (!await _context.Orders.AnyAsync(o => o.Id == item.OrderId))
                {
                    return new ResponseDto
                    {
                        IsSucceeded = false,
                        StatusCode = 400,
                        Message = "The Order you try to add this item on is not exist."
                    };
                }

                item.Order = await _context.Orders.FindAsync(item.OrderId);
                item.Product = await _context.Products.FindAsync(item.ProductId);
                if (item.Product != null)
                    item.TotalPrice = item.Quantity * item.Product.Price;

                item.Id = id;
                _context.Entry(result).CurrentValues.SetValues(item);
                var entity = _context.Entry(result);
                if (entity.State == EntityState.Modified)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = item
                    };
                }

                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to edit this order item."
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This Order item is not exist."
            };
        }

        public async Task<ResponseDto> DeleteOrderItem(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item != null)
            {
                _context.OrderItems.Remove(item);
                var entity = _context.Entry(item);
                if (entity.State == EntityState.Deleted)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = item
                    };
                }

                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to delete this item."
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This item already is not exist."
            };
        }
    }
}
