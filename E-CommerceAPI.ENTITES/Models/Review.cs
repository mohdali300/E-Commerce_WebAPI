﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
    }
}