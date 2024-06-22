using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ApiResponse<bool>>
    {
        private readonly FurnAppContext _db;

        public DeleteAddressCommandHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _db.Address
                .Include(a => a.Orders)
                .FirstOrDefaultAsync(a => a.AddressId == request.AddressId, cancellationToken);

            if (address == null)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    Message = "Address not found!",
                    Success = false
                };
            }

            // İlgili adresi referans eden tüm order kayıtlarını sil
            _db.Orders.RemoveRange(address.Orders);

            // Adresi sil
            _db.Address.Remove(address);
            await _db.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Address and related orders deleted successfully!",
                Success = true
            };
        }
    }
}