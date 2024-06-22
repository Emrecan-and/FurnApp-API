using FurnApp_API.DTO;
using FurnApp_API.Helper;
using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResponse<AddressDTO2>>
    {
        private readonly FurnAppContext db;

        public CreateAddressCommandHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<AddressDTO2>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if((request.BuildingNumber==null)||(request.City==null)|| (request.PostalCode == null) || (request.Street == null) || (request.District == null) || (request.HomeNumber==null)|| (request.Neighborhood==null))
            {
                return new ApiResponse<AddressDTO2>
                {
                    Data = null,
                    Message = "Required fields are missing!",
                    Success = false
                };
            }
            var address = new AddressDTO2
            {
                BuildingNumber = request.BuildingNumber,
                City = request.City,
                PostalCode=request.PostalCode,
                Street=request.Street,
                District=request.District,
                HomeNumber=request.HomeNumber,
                Neighborhood=request.Neighborhood
            };

            var adres = DtoConverter.AddressConverter(address);
            await db.Address.AddAsync(adres);
            await db.SaveChangesAsync();
            return new ApiResponse<AddressDTO2>()
            {
                Data=address,
                Message="Address was crote successfully!",
                Success=true
            };
        }
    }
}
