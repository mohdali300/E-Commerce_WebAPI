using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }=new List<Order>();
        public virtual ICollection<Cart>? Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Wishlist>? Wishlists { get; set; }= new List<Wishlist>();
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
