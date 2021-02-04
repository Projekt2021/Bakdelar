﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class Item
    {
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int ItemID { get; set; }

        public Item()
        {

        }
        public Item(string name, double price, int id)
        {
            ItemName = name;
            ItemPrice = price;
            ItemID = id;
        }
    }
}
