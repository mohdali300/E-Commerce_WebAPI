using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string? Message { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
