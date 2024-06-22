using FurnApp_API.Models;
using MediatR;
using System.Collections.Generic;
using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace FurnApp_API.Med.Queries
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResponse<List<Categories>>>
    {
        private readonly FurnAppContext _db;
        public GetCategoryQueryHandler(FurnAppContext db)
        {
            this._db = db;
        }
        public async Task<ApiResponse<List<Categories>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _db.Categories.ToListAsync();
            return new ApiResponse<List<Categories>>
            {
                Data = categories,
                Success = true,
                Message = "Categories were came successfully!"
            };
        }
    }
}