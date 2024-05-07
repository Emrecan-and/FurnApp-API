using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? UsersId { get; set; }
        public int? ProductId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Users Users { get; set; }
    }
}
