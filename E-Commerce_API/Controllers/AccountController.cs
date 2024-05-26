using E_CommerceAPI.ENTITES.DTOs.UserDTO;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
