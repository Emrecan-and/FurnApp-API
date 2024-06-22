using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ApiResponse<AddressDTO>>
    {
        private readonly FurnAppContext db;

        public UpdateAddressCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }

        public async Task<ApiResponse<AddressDTO>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await db.Address.FindAsync(request.AddressId);

            if (address == null)
            {
                return new ApiResponse<AddressDTO>
                {
                    Data = null,
                    Message = "Address not found!",
                    Success = false
                };
            }

            address.Neighborhood = request.Neighborhood;
            address.Street = request.Street;
            address.BuildingNumber = request.BuildingNumber;
            address.HomeNumber = request.HomeNumber;
            address.District = request.District;
            address.City = request.City;
            address.PostalCode = request.PostalCode;

            db.Address.Update(address);
            await db.SaveChangesAsync();

            var addressDTO = new AddressDTO
            {
                AddressId = address.AddressId,
                Neighborhood = address.Neighborhood,
                Street = address.Street,
                BuildingNumber = address.BuildingNumber,
                HomeNumber = address.HomeNumber,
                District = address.District,
                City = address.City,
                PostalCode = address.PostalCode
            };

            return new ApiResponse<AddressDTO>
            {
                Data = addressDTO,
                Message = "Address updated successfully!",
                Success = true
            };
        }
    }
}