using FurnApp_API.DTO;
using FurnApp_API.Helper;
using FurnApp_API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateOrdersCommandHandler : IRequestHandler<CreateOrdersCommand, ApiResponse<OrdersDTO2>>
    {
        private readonly FurnAppContext db;

        public CreateOrdersCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<OrdersDTO2>> Handle(CreateOrdersCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderDate == null  || request.UsersId == null ||  request.ProductId == null || request.AddressId == null)
            {
                return new ApiResponse<OrdersDTO2>
                {
                    Data = null,
                    Message = "Required fields are missing!",
                    Success = false
                };
            }

            var orderDto = new OrdersDTO2
            {
                OrderDate = request.OrderDate,
                CargoNo = request.CargoNo,
                UsersId = request.UsersId,
                ProductId = request.ProductId,
                AddressId = request.AddressId
            };

            var order = DtoConverter.OrdersConverter(orderDto);
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            return new ApiResponse<OrdersDTO2>
            {
                Data = orderDto,
                Message = "Order was created successfully!",
                Success = true
            };
        }
    }
}