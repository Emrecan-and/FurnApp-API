using FurnApp_API.Med.Commands;
using FurnApp_API.Models;
using FurnApp_API.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.CommandHandlers
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, ApiResponse<Token>>
    {
        private readonly FurnAppContext _db;
        private readonly IConfiguration _configuration;

        public UserDeleteCommandHandler(FurnAppContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<ApiResponse<Token>> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = _db.Users.FirstOrDefault(u => u.UsersMail == request.UsersMail);

            if (user == null)
            {
                return new ApiResponse<Token> { Data = null, Message = "User not found", Success = false };
            }

            // İlgili kullanıcıya ait Cart, Order ve Payment verilerini sil
            var carts = _db.Cart.Where(c => c.UsersId == user.UsersId).ToList();
            var orders = _db.Orders.Where(o => o.UsersId == user.UsersId).ToList();
            var payments = _db.Payment.Where(p => p.UsersId == user.UsersId).ToList();

            _db.Cart.RemoveRange(carts);
            _db.Orders.RemoveRange(orders);
            _db.Payment.RemoveRange(payments);


            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return new ApiResponse<Token> { Data = null, Message = "User and related data deleted successfully", Success = true };
        }
    }
}