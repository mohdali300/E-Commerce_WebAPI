using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs
{
    public class ReviewDto
    {
        public int Rate { get; set; }
        [MaxLength(250)]
        public string Comment { get; set; }
        public string ProductName { get; set; }
    }
}
