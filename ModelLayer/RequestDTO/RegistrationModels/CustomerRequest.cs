﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.Registration
{
    public class CustomerRequest
    {
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public long PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}