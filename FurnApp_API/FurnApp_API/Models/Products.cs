using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class Products
    {
        public Products()
        {
            Cart = new HashSet<Cart>();
            Orders = new HashSet<Orders>();
            ProductColors = new HashSet<ProductColors>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductStock { get; set; }
        public decimal? ProductPrice { get; set; }
        public byte[] ProductPicture { get; set; }
        public string ProductDescription { get; set; }
        public int? CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ProductColors> ProductColors { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
