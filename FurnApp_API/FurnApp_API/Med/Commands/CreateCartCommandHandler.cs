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
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ApiResponse<Cart>>
    {
        private readonly FurnAppContext db;

        public CreateCartCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<Cart>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(o => o.UsersId == request.UserId);
            var product = await db.Products.FirstOrDefaultAsync(o => o.ProductId == request.ProductId);
            if (request.ProductId == null || request.UserId == null)
            {
                var apiResponse = new ApiResponse<Cart>
                {
                    Data = null,
                    Message = "You have to enter productId and userId!",
                    Success = false
                };
                return apiResponse;
            }
            if(user==null || product == null)
            {
                var apiResponse = new ApiResponse<Cart>
                {
                    Data = null,
                    Message = "Wrong productId or userId!",
                    Success = false
                };
                return apiResponse;
            }
            var cart = new Cart { ProductId = request.ProductId, UsersId = request.UserId };
            await db.Cart.AddAsync(cart);
            await db.SaveChangesAsync();
            var apiResponse1 = new ApiResponse<Cart>
            {
                Data=null,
                Message = "Cart created succesfully!",
                Success = true
            };
            return apiResponse1;

        }
    }
}
