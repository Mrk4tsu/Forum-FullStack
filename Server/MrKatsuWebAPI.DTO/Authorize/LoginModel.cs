﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKatsuWebAPI.DTO.Authorize
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? ClientId { get; set; }
    }
}
