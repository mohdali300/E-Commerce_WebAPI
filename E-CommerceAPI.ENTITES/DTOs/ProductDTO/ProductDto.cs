using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs.ProductDTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
        public byte[]? Image { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        [JsonIgnore]
        public double? DiscountPercentage { get; set; }
        [JsonIgnore]
        public double? AfterDiscount { get; }
    }
}
