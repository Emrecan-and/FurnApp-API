using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, ApiResponse<Cart>>
    {
        private readonly FurnAppContext db;
        public DeleteCartCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }


        public async Task<ApiResponse<Cart>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await db.Cart.FindAsync(request.CartId);

            if (cart == null)
            {
                var apiResponse = new ApiResponse<Cart>
                {
                    Data = null,
                    Message = "Invalid CartId!",
                    Success = false
                };
                return apiResponse;
            }

            db.Cart.Remove(cart);
            await db.SaveChangesAsync();
            var apiResponse1 = new ApiResponse<Cart>
            {
                Data = null,
                Message = "Cart was deleted successfully!",
                Success = true
            };
            return apiResponse1;
        }
    }
}
