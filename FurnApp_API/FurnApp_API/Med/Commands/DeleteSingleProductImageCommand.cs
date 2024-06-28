using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class DeleteSingleProductImageCommand : IRequest<ApiResponse<SingleProductImageDto>>
    {
        public int ProductId { get; set; }
        public string Base64 { get; set; }
    }
}