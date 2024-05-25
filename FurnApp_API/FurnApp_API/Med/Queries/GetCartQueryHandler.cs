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
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, List<Cart>>
    {
        private readonly FurnAppContext db;

        public GetCartQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<List<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(o => o.UsersId == request.UserId);

            /* if (user == null)
             {
                 var apiResponse2 = new ApiResponseList<Cart>()
                 {
                     DataList = null,
                     Message = "Wrong userId!",
                     Success = false
                 };
                 return apiResponse2;
             }
             var cart = await db.Cart.Where(o => o.UsersId == request.UserId).ToListAsync(cancellationToken);
             if (!cart.Any())
             {
                 var apiResponse1 = new ApiResponseList<Cart>()
                 {
                     DataList = null,
                     Message = "We don't have any record for this user!",
                     Success = false
                 };
                 return apiResponse1;
             }*/
            var cart = await db.Cart.Where(o => o.UsersId == request.UserId).ToListAsync(cancellationToken);
            /*var apiResponse = List<Cart>()
            {
                DataList = cart,
                Message = "Request is successfully!",
                Success = true
            };
            return apiResponse;*/
            return cart;
        }
    }
}
