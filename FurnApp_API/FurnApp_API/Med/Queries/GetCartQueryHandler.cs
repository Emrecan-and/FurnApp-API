using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Queries
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ApiResponse<List<CartDTO>>>
    {
        private readonly FurnAppContext db;

        public GetCartQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<List<CartDTO>>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(o => o.UsersMail == request.UserMail);

            if (user == null)
             {
                 var apiResponse2 = new ApiResponse<List<CartDTO>>()
                 {
                     Data = null,
                     Message = "Wrong UserMail!",
                     Success = false
                 };
                 return apiResponse2;
             }
             var carts = await db.Cart.Where(o => o.UsersId==user.UsersId).ToListAsync(cancellationToken);
             if (!carts.Any())
             {
                 var apiResponse1 = new ApiResponse<List<CartDTO>>
                 {
                     Data = null,
                     Message = "We don't have any record for this user!",
                     Success = false
                 };
                 return apiResponse1;
             }
            var cartDTOs = new List<CartDTO>();
            foreach (var cart in carts)
            {
                var cartDto = new CartDTO()
                {
                    cartId = cart.CartId,
                    productId = (int)cart.ProductId,
                    userId = (int)cart.UsersId
                };
                cartDTOs.Add(cartDto);
            }
            return new ApiResponse<List<CartDTO>>()
            {
                Data = cartDTOs,
                Message = "Request is successfully!",
                Success = true
            };

        }
    }
}
