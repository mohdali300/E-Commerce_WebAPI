﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool IsSucceeded { get; set; }
        public object? Model { get; set; }
        public ICollection<object>? Models { get; set; }

    }
}
