using FurnApp_API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, ApiResponse<bool>>
    {
        private readonly FurnAppContext _db;

        public DeletePaymentCommandHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<bool>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _db.Payment.FindAsync(request.PaymentId);
            if (payment == null)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Payment not found!"
                };
            }

            _db.Payment.Remove(payment);
            await _db.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Payment deleted successfully!"
            };
        }
    }
}