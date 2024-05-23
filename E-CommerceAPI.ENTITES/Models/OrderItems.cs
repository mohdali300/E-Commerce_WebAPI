using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public double TotalPrice => (Quantity * Product.Price) + Order.ShippingCost;
        public double? Discount { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
