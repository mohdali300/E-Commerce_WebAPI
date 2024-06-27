using E_CommerceAPI.ENTITES.DTOs.UserDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = await _unitOfWork.Customers.RegisterAsync(dto);
                if (account != null)
                {
                    if (account.IsSucceeded)
                        return Ok(account);
                    return StatusCode(account.StatusCode, account.Message);
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = await _unitOfWork.Customers.LoginAsync(dto);
                if (account != null)
                {
                    if (account.IsSucceeded)
                        return StatusCode(account.StatusCode, account.Model);
                    return StatusCode(account.StatusCode, account.Model);
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);

        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordSettingDto dto)
        {
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.Customers.ChangePassword(dto);
                if(response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Message);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Customers.ResetPassword(dto);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Message);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("SendResetPasswordEmail")]
        public async Task<IActionResult> SendResetPasswordEmail(string Subject="Reset Password")
        {
            var currentUser=await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return BadRequest("Not authenticated user.");
            if (ModelState.IsValid)
            {
                await _unitOfWork.Mails.SendResetPasswordEmailAsync(currentUser,Subject);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> NewRefreshToken([FromBody]string email)
        {
            if (ModelState.IsValid)
            {
                var token=await _unitOfWork.Customers.GetRefreshToken(email);
                if (token != null)
                    return StatusCode(token.StatusCode, token.Model);
                return StatusCode(token.StatusCode,token.Model);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("EditProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody]UserDto dto, [FromHeader] string currentEmail)
        {
            if (ModelState.IsValid)
            {
                var account=await _unitOfWork.Customers.UpdateProfile(dto, currentEmail);
                if (account != null)
                    return StatusCode(account.StatusCode, account.Message);
                return StatusCode(account.StatusCode, account.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var response=await _unitOfWork.Customers.DeleteAccountAsync(dto);
                if(response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Message);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
