﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.ENTITES.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

    }
}
