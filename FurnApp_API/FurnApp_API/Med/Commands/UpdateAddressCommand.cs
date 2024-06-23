using MediatR;
using FurnApp_API.DTO;
using FurnApp_API.Models;

namespace FurnApp_API.Med.Commands
{
    public class UpdateAddressCommand : IRequest<ApiResponse<AddressDTO>>
    {
        public int AddressId { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string HomeNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}