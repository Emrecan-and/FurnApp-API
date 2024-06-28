using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, ApiResponse<ProductImageDTO>>
    {
        private readonly FurnAppContext _db;
        
        public DeleteProductImageCommandHandler(FurnAppContext db)
        {
            _db = db;
        }


        public async Task<ApiResponse<ProductImageDTO>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
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

            var productImages = _db.ProductImages.Where(x => x.ProductId == request.ProductId).ToList();

            if (productImages.Count == 0)
            {
                return new ApiResponse<ProductImageDTO>
                {
                    Data = null,
                    Message = "There is no image related with this product",
                    Success = false
                };
            }

            _db.ProductImages.RemoveRange(productImages);
            await _db.SaveChangesAsync();

            return new ApiResponse<ProductImageDTO>
            {
                Data = null,
                Message = "Images deleted successfully",
                Success = true
            };
        }
    }
}