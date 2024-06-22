using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ApiResponse<decimal>>
    {
        private readonly FurnAppContext _db;

        public CreatePaymentCommandHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<decimal>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(request.ProductId);

            if (product == null)
            {
                return new ApiResponse<decimal>
                {
                    Data = 0,
                    Message = "Product not found!",
                    Success = false
                };
            }

            if (product.ProductStock < request.Quantity)
            {
                return new ApiResponse<decimal>
                {
                    Data = 0,
                    Message = "Not enough stock!",
                    Success = false
                };
            }

            product.ProductStock -= request.Quantity;

            if (product.ProductStock < 0)
            {
                return new ApiResponse<decimal>
                {
                    Data =0,
                    Message = "Insufficient stock!",
                    Success = false
                };
            }

            var payment = new Payment
            {
                CreditCardNo = request.CreditCardNo,
                CardName = request.CardName,
                CardMonth = request.CardMonth,
                CardYear = request.CardYear,
                CardCvv = request.CardCvv,
                CargoPrice = request.CargoPrice,
                UsersId = request.UsersId,
                CargoCompany = request.CargoCompany
            };

            await _db.Payment.AddAsync(payment);
            await _db.SaveChangesAsync();

            // Ürün stoğunu güncelle
            await _db.SaveChangesAsync();
            decimal price =(decimal)((product.ProductPrice * request.Quantity)+request.CargoPrice);
  
            return new ApiResponse<decimal>
            {
                Data = price,
                Message = "Payment and stock update successful!",
                Success = true
            };
        }
    }
}