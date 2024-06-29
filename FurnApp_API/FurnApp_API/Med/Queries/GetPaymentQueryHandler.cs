using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Queries
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, ApiResponse<List<PaymentDTO>>>
    {
        private readonly FurnAppContext db;

        public GetPaymentsQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<List<PaymentDTO>>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await db.Payment
                .Include(p => p.Users)
                .ToListAsync(cancellationToken);
            var paymentDTOs = new List<PaymentDTO>();

            foreach (var payment in payments)
            {
                var paymentDto = new PaymentDTO
                {
                    PaymentId = payment.PaymentId,
                    CreditCardNo = payment.CreditCardNo,
                    CardName = payment.CardName,
                    CardMonth = payment.CardMonth,
                    CardYear = payment.CardYear,
                    CardCvv = payment.CardCvv,
                    CargoPrice = payment.CargoPrice,
                    UsersId = payment.UsersId,
                    UserFullName = payment.Users.UsersMail,
                    CargoCompany = payment.CargoCompany
                };
                paymentDTOs.Add(paymentDto);
            }

            return new ApiResponse<List<PaymentDTO>>
            {
                Data = paymentDTOs,
                Success = true,
                Message = "Payments were retrieved successfully!"
            };
        }
    }
}