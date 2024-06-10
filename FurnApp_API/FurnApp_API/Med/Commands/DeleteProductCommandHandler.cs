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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<String>>
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
                var apiResponse2 = new ApiResponse<String>()
                {
                    Data = null,
                    Message = "There is no data related with this ProductId!",
                    Success = false
                };
                return apiResponse2;
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return new ApiResponse<String>()
            {
                Data = null,
                Message = "Product was deleted successfully!",
                Success = true
            };
        }
    }
}
