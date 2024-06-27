using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{

    public class UpdateProductColorCommand : IRequest<ApiResponse<ProductColors>>
    {
        public int ColorId;
        public int ProductId;
    }
}
