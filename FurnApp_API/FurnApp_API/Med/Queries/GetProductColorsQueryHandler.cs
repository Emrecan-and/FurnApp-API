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
    public class GetProductColorsQueryHandler : IRequestHandler<GetProductColorsQuery, ApiResponse<List<ProductColors>>>
    {
        private readonly FurnAppContext db;
        public GetProductColorsQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<List<ProductColors>>> Handle(GetProductColorsQuery request, CancellationToken cancellationToken)
        {
            var ProductColors = await db.ProductColors.ToListAsync();
            return new ApiResponse<List<ProductColors>>
            {
                Data = ProductColors,
                Success = true,
                Message = "ProductColors were came successfully!"
            };
        }
    }
}
