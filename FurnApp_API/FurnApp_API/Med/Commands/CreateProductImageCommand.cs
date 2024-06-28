using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FurnApp_API.Med.Commands
{
    public class CreateProductImageCommand : IRequest<ApiResponse<ProductImageDTO>>
    {
        public int ProductId { get; set; }
        public IFormFileCollection FileCollection { get; set; }
    }
}