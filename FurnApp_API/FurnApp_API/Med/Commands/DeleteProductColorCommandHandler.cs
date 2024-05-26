using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteProductColorCommandHandler : IRequestHandler<DeleteProductColorCommand,ApiResponse<ProductColors>>
    {
        private readonly FurnAppContext db;
        public DeleteProductColorCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<ProductColors>> Handle(DeleteProductColorCommand request, CancellationToken cancellationToken)
        {
            var productColor = await db.ProductColors.FirstOrDefaultAsync(o => o.ColorId == request.colorId && o.ProductId == request.productId);
            if (productColor == null)
            {
                return new ApiResponse<ProductColors>()
                {
                    Data = null,
                    Message="There is no color related with these colorId and productId!",
                    Success=false
                };
            }
            db.ProductColors.Remove(productColor);
            await db.SaveChangesAsync();
            return new ApiResponse<ProductColors>()
            {
                Data = null,
                Message = "Color was deleted successfully!",
                Success = true
            };
        }
    }
}
