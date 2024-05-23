using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
        public double? DiscountPercentage { get; set; }
        public byte[]? Image { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public double? AfterDiscount => Price - (Price * (DiscountPercentage / 100));

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderItems>? orderItems { get; set; }
        public virtual ICollection<WishlistItems>? wishlistItems { get; set; }


    }
}
