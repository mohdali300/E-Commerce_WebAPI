using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet("Review/{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.Reviews.GetReview(id);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("CustomerReviews")]
        public async Task<IActionResult> GetCustomerReviews()
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Reviews.GetAllCustomerReviews(currentUser);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Product/{productId}/Reviews")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Reviews.GetAllProductReviews(productId);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("CustomerReviewOnProduct/{prodId}")]
        public async Task<IActionResult> GetCustomerReviewOnProduct(int prodId)
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Reviews.GetCustomerReviewOnProduct(currentUser,prodId);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(Review review)
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Reviews.AddReview(review,currentUser);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateReview")]
        public async Task<IActionResult> EditReview(int id,Review review)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Reviews.UpdateReview(id,review);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Reviews.DeleteReview(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Message);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
