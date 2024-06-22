using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateProductCommand : IRequest<ApiResponse<String>>
    {
        public string ProductName { get; set; }
        public int? ProductStock { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int? CategoryId { get; set; }
    }
}
