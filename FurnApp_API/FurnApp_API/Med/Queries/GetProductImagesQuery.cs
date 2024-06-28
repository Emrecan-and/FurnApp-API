using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Queries
{
    public class GetProductImagesQuery : IRequest<ApiResponse<ProductImageDTO>>
    {
        public int ProductId { get; set; }
    }
}