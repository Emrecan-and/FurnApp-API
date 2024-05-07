using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class ProductColors
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }

        public virtual Colors Color { get; set; }
        public virtual Products Product { get; set; }
    }
}
