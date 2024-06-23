using E_CommerceAPI.ENTITES.DTOs.WishlistDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet("Wishlist/{id}")]
        public async Task<IActionResult> GetWishlist(int id)
        {
            if(ModelState.IsValid)
            {
                var response = await _unitOfWork.Wishlists.GetWishlist(id);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Wishlists")]
        public async Task<IActionResult> GetAllWishlists()
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Wishlists.GetAllWishlists();
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddWishlist")]
        public async Task<IActionResult> AddWishlist(WishlistDto dto)
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Wishlists.AddWishlist(dto,currentUser);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateWishlist")]
        public async Task<IActionResult> EditWishlist(int id, WishlistDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Wishlists.UpdateWishlist(id, dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteWishlist")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Wishlists.DeleteWishlist(id);
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
