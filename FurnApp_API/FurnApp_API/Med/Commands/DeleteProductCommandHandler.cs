using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<string>>
    {
        private readonly FurnAppContext db;

        public DeleteProductCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await db.Products.FindAsync(request.ProductId);

            if (product == null)
            {
                return new ApiResponse<string>()
                {
                    Data = null,
                    Message = "There is no data related with this ProductId!",
                    Success = false
                };
            }

            // İlgili kayıtları sil
            db.Products.Remove(product);

            // Cart kayıtlarını sil
            var cartsToRemove = db.Cart.Where(c => c.ProductId == request.ProductId);
            db.Cart.RemoveRange(cartsToRemove);

            // Order kayıtlarını sil
            var ordersToRemove = db.Orders.Where(o => o.ProductId == request.ProductId);
            db.Orders.RemoveRange(ordersToRemove);

            // ProductColor kayıtlarını sil
            var productColorsToRemove = db.ProductColors.Where(pc => pc.ProductId == request.ProductId);
            db.ProductColors.RemoveRange(productColorsToRemove);

            await db.SaveChangesAsync();

            return new ApiResponse<string>()
            {
                Data = null,
                Message = "Product and related records were deleted successfully!",
                Success = true
            };
        }
    }
}