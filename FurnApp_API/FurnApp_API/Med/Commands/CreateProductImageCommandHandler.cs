using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class
        CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, ApiResponse<ProductImageDTO>>
    {
        private readonly FurnAppContext _db;

        public CreateProductImageCommandHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<ProductImageDTO>> Handle(CreateProductImageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var product = await _db.Products.FindAsync(request.ProductId);

                if (product == null)
                {
                    return new ApiResponse<ProductImageDTO>
                    {
                        Data = null,
                        Message = "There is no product related with this productID",
                        Success = false
                    };
                }
                
                _db.ProductImages.Where(pi => pi.ProductId == request.ProductId)
                    .ToList()
                    .ForEach(pi => _db.ProductImages.Remove(pi));
                
                List<string> base64Images = new List<string>();
                foreach (var formFile in request.FileCollection)
                {
                    if (formFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await formFile.CopyToAsync(ms);
                            var fileBytes = ms.ToArray();
                            var base64String = Convert.ToBase64String(fileBytes);
                            base64Images.Add(base64String);

                            var productImage = new ProductImage
                            {
                                ProductId = product.ProductId,
                                Base64 = base64String
                            };
                            await _db.ProductImages.AddAsync(productImage);
                        }
                    }
                }

                await _db.SaveChangesAsync();

                return new ApiResponse<ProductImageDTO>
                {
                    Data = new ProductImageDTO
                    {
                        ProductId = product.ProductId,
                        Base64Images = base64Images
                    },
                    Message = "Product images were created successfully!",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ApiResponse<ProductImageDTO>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}