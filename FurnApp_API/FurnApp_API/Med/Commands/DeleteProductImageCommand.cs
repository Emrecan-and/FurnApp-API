using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class DeleteProductImageCommand : IRequest<ApiResponse<ProductImageDTO>>
    {
        public int ProductId { get; set; }
    }
}