using System.Threading;
using System.Threading.Tasks;
using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurnApp_API.Med.Commands
{
    public class DeleteSingleProductImageCommandHandler : IRequestHandler<DeleteSingleProductImageCommand, ApiResponse<SingleProductImageDto>>
    {
        private readonly FurnAppContext _db;
        
        public DeleteSingleProductImageCommandHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<SingleProductImageDto>> Handle(DeleteSingleProductImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(request.ProductId);

            if (product == null)
            {
                return new ApiResponse<SingleProductImageDto>
                {
                    Data = null,
                    Message = "There is no product related with this productID",
                    Success = false
                };
            }

            var productImage = await _db.ProductImages
                .SingleOrDefaultAsync(pi => pi.ProductId == request.ProductId && pi.Base64 == request.Base64);

            if (productImage == null)
            {
                return new ApiResponse<SingleProductImageDto>
                {
                    Data = null,
                    Message = "There is no image related with this product",
                    Success = false
                };
            }

            _db.ProductImages.Remove(productImage);
            await _db.SaveChangesAsync();

            return new ApiResponse<SingleProductImageDto>
            {
                Data = null,
                Message = "Image deleted successfully",
                Success = true
            };
        }
    }
}