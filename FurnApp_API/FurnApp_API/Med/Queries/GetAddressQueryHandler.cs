using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Queries
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressesQuery, ApiResponse<List<AddressDTO>>>
    {
        private readonly FurnAppContext db;
        public GetAddressQueryHandler(FurnAppContext db)
        {
            this.db = db;
        }
        public async Task<ApiResponse<List<AddressDTO>>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await db.Address.ToListAsync();
            var addressDTOs = new List<AddressDTO>();
            foreach (var adress in addresses)
            {
                var addDto = new AddressDTO()
                {
                    AddressId=adress.AddressId,
                    BuildingNumber=adress.BuildingNumber,
                    PostalCode=adress.PostalCode,
                    City=adress.City,
                    District=adress.District,
                    HomeNumber=adress.HomeNumber,
                    Neighborhood=adress.Neighborhood,
                    Street = adress.Street,
                };
                addressDTOs.Add(addDto);
            }
            return new ApiResponse<List<AddressDTO>>
            {
                Data = addressDTOs,
                Success = true,
                Message = "Colors were came successfully!"
            };
        }
    }
}
