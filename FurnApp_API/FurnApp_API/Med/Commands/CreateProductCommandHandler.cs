using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<String>>
    {
        private readonly FurnAppContext db;
        public CreateProductCommandHandler(FurnAppContext db)
        {
            this.db = db;

        }

        public async Task<ApiResponse<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await db.Categories.FindAsync(request.CategoryId);
            if (category == null)
            {
                return new ApiResponse<String>()
                {
                    Data = null,
                    Message = "There is no category related with this categoryID",
                    Success = false

                };
            }
            var product = new Products
            {
                CategoryId = request.CategoryId,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.ProductPrice,
                ProductStock = request.ProductStock
            };
            db.Products.Add(product);
           await db.SaveChangesAsync();
            return new ApiResponse<String>()
            {
                Data = product.ProductId.ToString(),
                Message = "Product was created successfully",
                Success = true
            };
        }
    }
}
