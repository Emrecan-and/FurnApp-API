using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, ApiResponse<Colors>>
    {
        private readonly FurnAppContext db;
        public DeleteColorCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<Colors>> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            var color = await db.Colors.FindAsync(request.ColorId);

            if (color == null)
            {
                return new ApiResponse<Colors>()
                {
                    Data = null,
                    Message = "There is no data related with this ColorId!",
                    Success = false
                };
            }

            // İlgili ProductColor kayıtlarını bul ve sil
            var productColors = db.ProductColors.Where(pc => pc.ColorId == request.ColorId);
            db.ProductColors.RemoveRange(productColors);

            // Color'ı sil
            db.Colors.Remove(color);
            await db.SaveChangesAsync();

            return new ApiResponse<Colors>()
            {
                Data = null,
                Message = "Color and related ProductColors were deleted successfully!",
                Success = true
            };
        }
    }
}