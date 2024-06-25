using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishlistItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Item/{id}")]
        public async Task<IActionResult> GetWishlistItem(int id)
        {
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.WishlistItems.GetWishlistItem(id);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("WishlistsItems")]
        public async Task<IActionResult> GetAllWishlistsItems()
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.WishlistItems.GetAllWishlistItems();
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ItemsInList/{listId}")]
        public async Task<IActionResult> GetItemsInList(int listId)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.WishlistItems.GetItemsInWishlist(listId);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItemToList(WishlistItems item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.WishlistItems.AddWishlistItem(item);
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
        public async Task<IActionResult> EditCartItem(int id, WishlistItems item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.WishlistItems.UpdateWishlistItem(id, item);
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
                var response = await _unitOfWork.WishlistItems.DeleteWishlistItem(id);
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
