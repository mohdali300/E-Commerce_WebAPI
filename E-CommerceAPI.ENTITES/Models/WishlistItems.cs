using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class WishlistItems
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int? WishlistId { get; set; }
        public int? ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("WishlistId")]
        public virtual Wishlist? Wishlist { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
