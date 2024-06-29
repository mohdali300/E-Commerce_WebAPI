using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs.PaymentDTO
{
    public class PaymentDto
    {
        public string Currency { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
