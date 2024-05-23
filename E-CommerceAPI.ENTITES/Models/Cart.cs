using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public virtual ICollection<CartItems>? cartItems { get; set; }
    }
}
