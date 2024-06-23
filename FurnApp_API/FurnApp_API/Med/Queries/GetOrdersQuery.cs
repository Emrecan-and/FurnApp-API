using MediatR;
using FurnApp_API.DTO;
using System.Collections.Generic;
using FurnApp_API.Models;

namespace FurnApp_API.Med.Queries
{
    public class GetOrdersQuery : IRequest<ApiResponse<List<OrdersDTO2>>>
    {
    }
}