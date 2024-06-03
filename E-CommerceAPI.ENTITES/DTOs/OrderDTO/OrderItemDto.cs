using E_CommerceAPI.ENTITES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs.OrderDTO
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice {  get; set; }
        public double? Discount { get; set; }
    }
}
