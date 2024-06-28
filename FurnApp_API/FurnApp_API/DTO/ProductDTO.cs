using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductStock { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int? CategoryId { get; set; }
        public string imageBase64 { get; set; }
    }
}
