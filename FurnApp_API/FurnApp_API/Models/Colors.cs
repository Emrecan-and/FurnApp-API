using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class Colors
    {
        public Colors()
        {
            ProductColors = new HashSet<ProductColors>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<ProductColors> ProductColors { get; set; }
    }
}
