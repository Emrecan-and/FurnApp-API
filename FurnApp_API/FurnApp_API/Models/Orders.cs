using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CargoNo { get; set; }
        public int? UsersId { get; set; }
        public int? ProductId { get; set; }
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users Users { get; set; }
    }
}
