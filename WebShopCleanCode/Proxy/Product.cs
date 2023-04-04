using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode.Proxy
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int NrInStock { get; set; }
        public Product(string name, int price, int nrInStock)
        {
            Name = name;
            Price = price;
            NrInStock = nrInStock;
        }
    }
}
