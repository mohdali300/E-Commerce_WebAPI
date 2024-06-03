using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.UserDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class AccountRepository:GenericRepository<ApplicationUser>,IAccountRepository
    {
        private readonly ECommerceDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRepository(ECommerceDbContext context, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ResponseDto> RegisterAsync(RegisterDto register)
        {
            if (await _userManager.FindByNameAsync(register.UserName) != null)
            {
                return new ResponseDto
                {
                    Message = "This user already exists!",
                    IsSucceeded = false,
                    StatusCode = 400
                };
            }

            var user = _mapper.Map<ApplicationUser>(register);

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)     
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, \n";
                }
                return new ResponseDto
                {
                    Message = errors,
                    IsSucceeded = false,
                    StatusCode = 400
                };
            }

            await _userManager.AddToRoleAsync(user, "Customer");
            var JwtToken =await CreateToken(user);
            return new ResponseDto
            {
                Message = "Account createdSuccessfully.",
                IsSucceeded = true,
                StatusCode = 200,
                Model = new
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                    UserName = user.UserName,
                }
            };

        }

        public async Task<ResponseDto> LoginAsync(LoginDto dto)
        {
            var user =await _userManager.FindByEmailAsync(dto.Email);
            if(user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded=false,
                    Model = new AuthDto
                    {
                        Token=string.Empty,
                        Email=string.Empty,
                        Role=string.Empty,
                        UserName=string.Empty,
                        IsAuthenticated = false,
                        Message = "Invalid Email or Password!",
                    }
                };
            }

            var JwtToken =await CreateToken(user);
            var refreshToken = "";
            DateTime refreshTokenExpiration;

            if (user.RefreshTokens!.Any(t => t.IsActive))
            {
                var activeToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                refreshToken = activeToken.Token;
                refreshTokenExpiration = activeToken.ExpiresOn;
            }
            else
            {
                var RefreshToken = CreateRefreshToken();
                refreshToken = RefreshToken.Token;
                refreshTokenExpiration = RefreshToken.ExpiresOn;
                user.RefreshTokens.Add(RefreshToken);
                await _userManager.UpdateAsync(user);
            }

            return new ResponseDto
            {
                StatusCode=200,
                IsSucceeded=true,
                Model= new AuthDto
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                    UserName = user.UserName,
                    Email = user.Email,
                    Message = "Signed in Successfully",
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = refreshTokenExpiration
                }
                
            };
        }

        

        public async Task<ResponseDto> GetRefreshToken(string Email)
        {
            var user=await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Model = new AuthDto
                    {
                        Message = "Invalid Email!",
                        IsAuthenticated = false,
                        RefreshToken = string.Empty,
                        Token = string.Empty,
                        Email = string.Empty,
                        UserName = string.Empty
                    }
                };
            }

            var ActiveToken = user.RefreshTokens!.FirstOrDefault(t => t.IsActive==true);

            if (ActiveToken!=null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Model = new AuthDto
                    {
                        Message = "There is a Token still active <3",
                        IsAuthenticated = false,
                        RefreshToken = string.Empty,
                        Token = string.Empty,
                        Email = string.Empty,
                        UserName = string.Empty
                    }
                };
                
            }

            var refreshToken=CreateRefreshToken();
            var Token =await CreateToken(user);
            user.RefreshTokens!.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model = new AuthDto
                {
                    IsAuthenticated = true,
                    RefreshToken = refreshToken.Token,
                    Token = new JwtSecurityTokenHandler().WriteToken(Token),
                    Email = user.Email,
                    RefreshTokenExpiration = refreshToken.ExpiresOn,
                    UserName = user.UserName,
                    Message=""
                }
            };

        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userClaim = _httpContextAccessor.HttpContext.User;

            return await _userManager.GetUserAsync(userClaim);
        }

        public async Task<ResponseDto> UpdateProfile(UserDto dto,string currentEmail)
        {
            var user =await _userManager.FindByEmailAsync(currentEmail);
            if (user == null)
            {
                return new ResponseDto
                {
                    Message = "An error occured, user not found!",
                    IsSucceeded = false,
                    StatusCode = 400,
                };
            }

            try
            {
                //user = _mapper.Map<ApplicationUser>(dto);
                user.FirstName = dto.FirstName;
                user.PhoneNumber = dto.PhoneNumber;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.Address=dto.Address;
                await _userManager.UpdateAsync(user);
                return new ResponseDto
                {
                    Message = "Updated your profile successfully.",
                    IsSucceeded = true,
                    StatusCode = 200,
                };
            }
            catch
            {
                return new ResponseDto
                {
                    Message = "Cannot update or edit your profile, try again!",
                    IsSucceeded = false,
                    StatusCode = 400,
                };
            }

        }

        public async Task<ResponseDto> DeleteAccountAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new ResponseDto
                {
                    Message = "Email or current password is INCORRECT!",
                    IsSucceeded = false,
                    StatusCode = 400,
                };
            }

            var result=await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new ResponseDto
                {
                    Message = "Failed to delete your account, try again later!",
                    IsSucceeded = false,
                    StatusCode = 400,
                };
            }
            return new ResponseDto
            {
                Message = "Your Account deleted Successfully,we hope you will back again.",
                IsSucceeded = true,
                StatusCode = 200,
            };
        }

        public async Task<ResponseDto> ChangePassword(PasswordSettingDto dto)
        {
            var user=await _userManager.FindByEmailAsync(dto.Email);
            if(user==null || await _userManager.CheckPasswordAsync(user,dto.CurrentPassword))
            {
                return new ResponseDto
                {
                    Message = "Email or current password is INCORRECT!",
                    IsSucceeded = false,
                    StatusCode = 400,
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if(!result.Succeeded)
            {
                return new ResponseDto
                {
                    Message = "Failed to change password, try again!",
                    IsSucceeded = false,
                    StatusCode = 400,
                };
            }
            return new ResponseDto
            {
                Message = "Your Password Changed Successfully.",
                IsSucceeded = true,
                StatusCode = 200,
            };
        }


        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var JwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                signingCredentials: signing,
                expires: DateTime.Now.AddHours(6)
                );
            return JwtToken;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddHours(6),
                CreatedOn = DateTime.UtcNow
            };
        }

        
    }
}
