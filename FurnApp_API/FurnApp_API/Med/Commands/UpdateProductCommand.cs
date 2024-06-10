using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UpdateProductCommand : IRequest<ApiResponse<String>>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductStock { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int? CategoryId { get; set; }
    }
}
