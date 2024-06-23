using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("AllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            if(ModelState.IsValid)
            {
                var response = await _unitOfWork.CartItems.GetAllCartsItems();
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Item/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.CartItems.GetCartItem(id);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ItemsInCart")]
        public async Task<IActionResult> GetItemsInCart(int cartId)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.CartItems.GetItemsInCart(cartId);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItemToCart(CartItems item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.CartItems.AddItemToCart(item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> EditCartItem(int id,CartItems item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.CartItems.UpdateCartItem(id, item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.CartItems.DeleteItemFromCart(id);
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
