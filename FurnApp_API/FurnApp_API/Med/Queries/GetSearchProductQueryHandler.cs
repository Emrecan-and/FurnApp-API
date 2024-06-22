using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Queries
{
    public class GetSearchProductQueryHandler : IRequestHandler<GetSearchProductQuery, ApiResponse<List<ProductDTO>>>
    {
        private readonly FurnAppContext db;
        public GetSearchProductQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<List<ProductDTO>>> Handle(GetSearchProductQuery request, CancellationToken cancellationToken)
        {
            if (request.ProductName == null)
            {
                return new ApiResponse<List<ProductDTO>>()
                {
                    Data = null,
                    Message = "Product name cannot be null!",
                    Success = false
                };
            }
            var products= await db.Products.Where(p => p.ProductName.Contains(request.ProductName.ToLower())).ToListAsync();

            var Products = new List<ProductDTO>();
            foreach (var product in products)
            {
                var productDto = new ProductDTO()
                {
                    ProductId = product.ProductId,
                    ProductDescription = product.ProductDescription,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductStock = product.ProductStock,
                    CategoryId = product.CategoryId
                };
                Products.Add(productDto);
            }
            return new ApiResponse<List<ProductDTO>>()
            {
                Data = Products,
                Message = "Products was came successfully!",
                Success = true
            };
        }
    }
}
