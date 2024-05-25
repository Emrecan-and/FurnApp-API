﻿using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateCartCommand : IRequest<ApiResponse<Cart>>
    {
        public int ProductId;

        public int UserId;
    }
}
