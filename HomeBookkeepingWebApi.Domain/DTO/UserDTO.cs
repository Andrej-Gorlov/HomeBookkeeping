﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Domain.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public HashSet<СreditСardDTO>? СreditСard { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
