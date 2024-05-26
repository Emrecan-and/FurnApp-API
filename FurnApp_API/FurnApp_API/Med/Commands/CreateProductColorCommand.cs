using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateProductColorCommand : IRequest<ApiResponse<ProductColorDTO>>
    {
        public  int productId { get; set; }

        public int colorId { get; set; }
    }
}
