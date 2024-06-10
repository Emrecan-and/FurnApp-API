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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ApiResponse<List<ProductDTO>>>
    {
        private readonly FurnAppContext db;
        public GetProductsQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<List<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var filteredProducts = new List<Products>();
            if (request.categoryIds.Count != 0)
            {
                foreach (var categoryId in request.categoryIds)
                {
                    var productsInCategory = await db.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
                    filteredProducts.AddRange(productsInCategory);
                }

            }
            if (request.minPrice != null || request.maxPrice != null)
            {
                if (request.categoryIds.Count != 0)
                {
                    filteredProducts = filteredProducts.Where(p => (p.ProductPrice > request.minPrice) && (p.ProductPrice < request.maxPrice)).ToList();
                }
                else
                {
                    var productsInCategory = await db.Products.Where(p => (p.ProductPrice > request.minPrice) && (p.ProductPrice < request.maxPrice)).ToListAsync();
                    filteredProducts.AddRange(productsInCategory);
                }
            }
            if(request.minPrice==null && request.maxPrice==null && request.categoryIds.Count == 0)
            {
                filteredProducts = await db.Products.ToListAsync();
            }
            if (request.sortBy != null)
            {
                switch (request.sortBy.ToLower())
                {
                    case "asc":
                        filteredProducts = filteredProducts.OrderBy(p => p.ProductPrice).ToList();
                        break;
                    case "desc":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.ProductPrice).ToList();
                        break;
                }
            }
            var products = new List<ProductDTO>();
            foreach (var product in filteredProducts)
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
                products.Add(productDto);
            }
            return new ApiResponse<List<ProductDTO>>()
            {
                Data=products,
                Message="Products was came successfully!",
                Success=true
            };
        }
     
    }
}
