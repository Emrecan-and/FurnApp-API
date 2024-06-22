using FurnApp_API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, ApiResponse<Colors>>
    {
        private readonly FurnAppContext db;
        public UpdateColorCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<Colors>> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            var color = await db.Colors.FindAsync(request.ColorId);
            if (color == null)
            {
                var apiResponse2 = new ApiResponse<Colors>()
                {
                    Data = null,
                    Message = "There is no data related with this ColorId!",
                    Success = false
                };
                return apiResponse2;
            }

            color.ColorName = request.NewColorName;
            db.Colors.Update(color);
            await db.SaveChangesAsync();
            return new ApiResponse<Colors>()
            {
                Data = color,
                Message = "Color was updated successfully!",
                Success = true
            };

        }
    }
}
