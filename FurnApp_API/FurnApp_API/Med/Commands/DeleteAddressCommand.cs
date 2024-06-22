using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class DeleteAddressCommand : IRequest<ApiResponse<bool>>
    {
        public int AddressId { get; set; }
    }
}