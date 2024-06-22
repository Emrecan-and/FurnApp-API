using MediatR;
using FurnApp_API.Models;
using FurnApp_API.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FurnApp_API.Med.Queries
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResponse<List<OrdersDTO2>>>
    {
        private readonly FurnAppContext db;

        public GetOrdersQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<List<OrdersDTO2>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await db.Orders
                                 .Include(o => o.Users)
                                 .Include(o => o.Product)
                                 .Include(o => o.Address)
                                 .Select(o => new OrdersDTO2
                                 {
                                     OrderId = o.OrderId,
                                     OrderDate = o.OrderDate,
                                     CargoNo = o.CargoNo,
                                     UsersId = o.UsersId,
                                     ProductId = o.ProductId,
                                     AddressId = o.AddressId
                                 }).ToListAsync();

            return new ApiResponse<List<OrdersDTO2>>
            {
                Data = orders,
                Message = "Orders retrieved successfully",
                Success = true
            };
        }
    }
}