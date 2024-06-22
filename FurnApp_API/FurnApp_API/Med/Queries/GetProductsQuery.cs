using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System.Collections.Generic;

namespace FurnApp_API.Med.Queries
{
    public class GetProductsQuery : IRequest<ApiResponse<List<ProductDTO>>>
    {
        public List<int> categoryIds { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public string sortBy { get; set; }
    }
}
