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
                var orders = await _unitOfWork.Orders.GetAllOrdersItems();
                if (orders.IsSucceeded)
                    return StatusCode(orders.StatusCode, orders.Model);
                return StatusCode(orders.StatusCode, orders.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
