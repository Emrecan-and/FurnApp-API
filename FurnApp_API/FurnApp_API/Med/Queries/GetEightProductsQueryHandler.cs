using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Queries
{
    public class GetEightProductsQueryHandler : IRequestHandler<GetEightProductsQuery, ApiResponse<List<ProductDTO>>>
    {
        private readonly FurnAppContext db;
        public GetEightProductsQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<List<ProductDTO>>> Handle(GetEightProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await db.Products.OrderBy(p=>p.ProductId).Take(8).ToListAsync();
            var productDTOs = new List<ProductDTO>();
            foreach (var product in products)
            {
                var productDto = new ProductDTO()
                {
                    CategoryId=product.CategoryId,
                    ProductDescription=product.ProductDescription,
                    ProductId=product.ProductId,
                    ProductName=product.ProductName,
                    ProductPrice=product.ProductPrice,
                    ProductStock=product.ProductStock
                };
                productDTOs.Add(productDto);
            }
            return new ApiResponse<List<ProductDTO>>
            {
                Data = productDTOs,
                Success = true,
                Message = "Products were came successfully!"
            };
        }
    }
}
