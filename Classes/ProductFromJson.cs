using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class ProductFromJson
    {
#nullable enable
        public int Id { get; set; }
#nullable disable
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ImageLink { get; set; }
        public double SalePrice { get; set; }
#nullable enable
        public string? Secret { get; set; } = null;

    }
}
