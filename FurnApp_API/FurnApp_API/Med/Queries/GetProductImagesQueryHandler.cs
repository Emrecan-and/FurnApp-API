using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnApp_API.Med.Queries
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQuery, ApiResponse<ProductImageDTO>>
    {
        private readonly FurnAppContext _db;

        public GetProductImagesQueryHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<ProductImageDTO>> Handle(GetProductImagesQuery request,
            CancellationToken cancellationToken)
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

            var productImages = await _db.ProductImages
                .Where(pi => pi.ProductId == request.ProductId)
                .ToListAsync();

            if (productImages == null || productImages.Count == 0)
            {
                return new ApiResponse<ProductImageDTO>
                {
                    Data = null,
                    Message = "There is no image related with this product",
                    Success = false
                };
            }
            
            var base64Images = productImages.Select(pi => pi.Base64).ToList();
            var productId = product.ProductId;
            
            return new ApiResponse<ProductImageDTO>
            {
                Data = new ProductImageDTO
                {
                    ProductId = productId,
                    Base64Images = base64Images
                },
                Message = "Images are successfully fetched",
                Success = true
            };
        }
    }
}