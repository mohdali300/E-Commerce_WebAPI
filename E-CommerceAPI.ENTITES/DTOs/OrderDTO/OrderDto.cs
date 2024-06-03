using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs.OrderDTO
{
    public class OrderDto
    {
        public DateTime? Date { get; set; }
        public string? Status { get; set; }

        public int? TrackNumber { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingMethod { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public double ShippingCost { get; set; }
    }
}
