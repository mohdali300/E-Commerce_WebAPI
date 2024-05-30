using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs.ProductDTO
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
        public byte[]? Image { get; set; }

        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        public double? DiscountPercentage { get; set; }
        public double? AfterDiscount { get; }
    }
}
