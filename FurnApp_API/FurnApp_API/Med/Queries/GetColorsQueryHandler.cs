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
    public class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, ApiResponse<List<Colors>>>
    {
        private readonly FurnAppContext db;
        public GetColorsQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<List<Colors>>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
        {
            var colors = await db.Colors.ToListAsync();
            return new ApiResponse<List<Colors>>
            {
                Data = colors,
                Success = true,
                Message = "Colors were came successfully!"
            };
        }
    }
}
