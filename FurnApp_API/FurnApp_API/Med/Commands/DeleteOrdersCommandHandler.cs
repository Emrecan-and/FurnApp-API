using MediatR;
using FurnApp_API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteOrdersCommandHandler : IRequestHandler<DeleteOrdersCommand, ApiResponse<bool>>
    {
        private readonly FurnAppContext db;

        public DeleteOrdersCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteOrdersCommand request, CancellationToken cancellationToken)
        {
            var order = await db.Orders.FindAsync(request.OrderId);

            if (order == null)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    Message = "Order not found!",
                    Success = false
                };
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Order deleted successfully!",
                Success = true
            };
        }
    }
}