using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateProductColorCommandHandler : IRequestHandler<CreateProductColorCommand, ApiResponse<ProductColorDTO>>
    {
        private readonly FurnAppContext db;
        public CreateProductColorCommandHandler(FurnAppContext db)
        {
            this.db = db;
           
        }

        public async Task<ApiResponse<ProductColorDTO>> Handle(CreateProductColorCommand request, CancellationToken cancellationToken)
        {
            var color = await db.Colors.FindAsync(request.colorId);
            var product = await db.Products.FindAsync(request.productId);
            if (color == null)
            {
                return new ApiResponse<ProductColorDTO>()
                {
                    Data = null,
                    Message = "There is no color related with this colorID",
                    Success = false
                };
            }
            if (product == null)
            {
                return new ApiResponse<ProductColorDTO>()
                {
                    Data = null,
                    Message = "There is no product related with this productID",
                    Success = false
                };
            }
            var productColor = new ProductColors()
            {
                ColorId = request.colorId,
                ProductId = request.productId
            };
            var productColorDTO = new ProductColorDTO()
            {
                ColorId = request.colorId,
                ProductId = request.productId
            };
            await db.ProductColors.AddAsync(productColor);
            await db.SaveChangesAsync();

            return new ApiResponse<ProductColorDTO>()
            {
                Data = productColorDTO,
                Message = "ProductColor was created successfully!",
                Success = true
            };
        }
    }
}
