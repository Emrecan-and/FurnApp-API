using FurnApp_API.Models;
using MediatR;

namespace FurnApp_API.Med.Commands
{
    public class DeletePaymentCommand : IRequest<ApiResponse<bool>>
    {
        public int PaymentId { get; set; }
    }
}