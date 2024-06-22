using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class DeleteOrdersCommand : IRequest<ApiResponse<bool>>
    {
        public int OrderId { get; set; }
    }
}