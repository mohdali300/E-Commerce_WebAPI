using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
        [JsonIgnore]
        public double? DiscountPercentage { get; set; }
        public byte[]? Image { get; set; }
        //public string? Category { get; set; }
        //public string? Brand { get; set; }
    }
}
