using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
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
                var apiResponse2 = new ApiResponse<Colors>()
                {
                    Data = null,
                    Message = "There is no data related with this ColorId!",
                    Success = false
                };
                return apiResponse2;
            }

            db.Colors.Remove(color);
            await db.SaveChangesAsync();
            return new ApiResponse<Colors>()
            {
                Data = null,
                Message = "Color was deleted successfully!",
                Success = true
            };
        }
    }
}
