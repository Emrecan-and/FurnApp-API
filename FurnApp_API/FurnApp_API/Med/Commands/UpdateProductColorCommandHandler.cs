using FurnApp_API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UpdateProductColorCommandHandler : IRequestHandler<UpdateProductColorCommand, ApiResponse<ProductColors>>
    {
        private readonly FurnAppContext db;
        public UpdateProductColorCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<ProductColors>> Handle(UpdateProductColorCommand request, CancellationToken cancellationToken)
        {
            var productColor = await db.ProductColors.FindAsync(request.ProductId);
            if (productColor == null)
            {
                var apiResponse2 = new ApiResponse<ProductColors>()
                {
                    Data = null,
                    Message = "There is no data related with this ProductId and ColorId!",
                    Success = false
                };
                return apiResponse2;
            }

            productColor.ColorId = request.ColorId;
            db.ProductColors.Update(productColor);
            await db.SaveChangesAsync();
            return new ApiResponse<ProductColors>()
            {
                Data = productColor,
                Message = "Color was updated successfully!",
                Success = true
            };

        }
    }
}
