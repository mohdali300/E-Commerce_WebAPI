using E_CommerceAPI.ENTITES.DTOs.OrderDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            if(ModelState.IsValid)
            {
                var orders=await _unitOfWork.Orders.GetCustomerOrders();
                if (orders.IsSucceeded)
                    return StatusCode(orders.StatusCode, orders.Model);
                return StatusCode(orders.StatusCode, orders.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Order/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            if (ModelState.IsValid)
            {
                var order = await _unitOfWork.Orders.GetOrderById(id);
                if (order.IsSucceeded)
                    return StatusCode(order.StatusCode, order.Model);
                return StatusCode(order.StatusCode, order.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("OrderItem/{id}")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            if (ModelState.IsValid)
            {
                var order = await _unitOfWork.Orders.GetOrderItems(id);
                if (order.IsSucceeded)
                    return StatusCode(order.StatusCode, order.Model);
                return StatusCode(order.StatusCode, order.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Items")]
        public async Task<IActionResult> GetAllItems()
        {
            if (ModelState.IsValid)
            {
                var orders = await _unitOfWork.Orders.GetAllItems();
                if (orders.IsSucceeded)
                    return StatusCode(orders.StatusCode, orders.Model);
                return StatusCode(orders.StatusCode, orders.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ItemsInOrder/{orderId}")]
        public async Task<IActionResult> GetItemsInOrder(int orderId)
        {
            if (ModelState.IsValid)
            {
                var Items = await _unitOfWork.Orders.GetItemsInOrder(orderId);
                if (Items.IsSucceeded)
                    return StatusCode(Items.StatusCode, Items.Model);
                return StatusCode(Items.StatusCode, Items.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto dto)
        {
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.Orders.AddOrder(dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(OrderItems item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.OrderItems.AddOrderItem(item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("EditOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Orders.UpdateOrder(id,dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("EditOrderItem/{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItems item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Orders.UpdateOrderItem(id, item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Orders.DeleteOrder(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteOrderItem/{id}")]
        public async Task<IActionResult> CancelOrderItem(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Orders.DeleteOrderItem(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
