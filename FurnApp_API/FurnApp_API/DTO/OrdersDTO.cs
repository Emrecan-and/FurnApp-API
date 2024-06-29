using System;

namespace FurnApp_API.DTO
{
    public class OrdersDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CargoNo { get; set; }
        public int? UsersId { get; set; }
        public int? ProductId { get; set; }
        public int? AddressId { get; set; }
    }

    public class OrdersDTO2
    {
        public DateTime OrderDate { get; set; }
        public int? CargoNo { get; set; }
        public int? UsersId { get; set; }
        public string UserFullName { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? AddressId { get; set; }
        public string AddressName { get; set; }
        public int OrderId { get; internal set; }
    }
}