using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System.Collections.Generic;

namespace FurnApp_API.Med.Queries
{
    public class GetEightProductsQuery : IRequest<ApiResponse<List<ProductDTO>>>
    {

    }
}
