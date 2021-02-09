using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class ProductFromJson
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ICollection<ExpandoObject> ProductImages { get; set; }
        public ExpandoObject Category { get; set; }
        public double SalePrice { get; set; }


    }
}
