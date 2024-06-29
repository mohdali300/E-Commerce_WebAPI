using E_CommerceAPI.ENTITES.DTOs.PaymentDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> CreateCheckOutSession(PaymentDto dto)
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return BadRequest("User not authenticated");
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.Payments.CreateCheckoutSession(dto,currentUser);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode,response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }


        [HttpGet("Payment/{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Payments.GetPayment(id);
                if (response.IsSucceeded)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("AllPayment")]
        public async Task<IActionResult> GetAllPayments()
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Payments.GetAllPayments();
                if (response.IsSucceeded)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Payments.DeletePayment(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        //[HttpGet("success")]
        //public IActionResult Success()
        //{
        //    return Ok("Succeeded");
        //}

        //[HttpGet("cancel")]
        //public IActionResult Cancel()
        //{
        //    return Ok("Canceled");
        //}


    }
}
