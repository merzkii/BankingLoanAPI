﻿using Application.DTO.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth
{
    public class LoginAdminCommand:IRequest<LoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
