using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<WishlistItems>? wishlistItems { get; set; }=new List<WishlistItems>();

    }
}
