using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bakdelar.Classes
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductImageID { get; set; }
        public ProductImage ProductImage { get; set; }
        public int CategoryID { get; set; }
        public double SalePrice { get; set; }

    }
}
