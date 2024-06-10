using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<String>>
    {
        private readonly FurnAppContext db;
        public UpdateProductCommandHandler(FurnAppContext db)
        {
            this.db = db;

        }
        public async Task<ApiResponse<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
            var product = await db.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                var apiResponse2 = new ApiResponse<String>()
                {
                    Data = null,
                    Message = "There is no data related with this productId!",
                    Success = false
                };
                return apiResponse2;
            }
            product.ProductName = request.ProductName;
            product.ProductDescription = request.ProductDescription;
            product.ProductPrice = request.ProductPrice;
            product.ProductStock = request.ProductStock;
            product.CategoryId = request.CategoryId;

            db.Products.Update(product);
            await db.SaveChangesAsync();
            return new ApiResponse<String>()
            {
                Data = null,
                Message = "Product was updated successfully!",
                Success = true
            };
        }
    }
}
