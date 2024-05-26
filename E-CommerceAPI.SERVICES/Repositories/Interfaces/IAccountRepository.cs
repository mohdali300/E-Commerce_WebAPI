using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.UserDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IAccountRepository:IGenericRepository<ApplicationUser>
    {
        public Task<ResponseDto> LoginAsync(LoginDto login);
        public Task<ResponseDto> GetRefreshToken(string email);
        public Task<ResponseDto> RegisterAsync(RegisterDto register);
        public Task<ResponseDto> DeleteAccountAsync(LoginDto dto);
        public Task<ResponseDto> ChangePassword(PasswordSettingDto password);
        public Task<ResponseDto> UpdateProfile(UserDto user,string currentEmail);

    }
}
