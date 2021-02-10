using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public string ImageUrl { get; set; }
        public int? ProductID { get; set; }
#nullable enable
        public virtual Product? Product { get; set; }
    }
}
