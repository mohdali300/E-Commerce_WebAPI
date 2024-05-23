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

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<Wishlist>? Wishlists { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }

    }
}
