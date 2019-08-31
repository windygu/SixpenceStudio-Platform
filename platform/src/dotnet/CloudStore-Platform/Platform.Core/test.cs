
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Core
{
    class test
    {

    }

    public class Product
    {
        readonly string name;
        public string Name
        {
            get { return name; }
        }

        readonly decimal price;
        public decimal Price
        {
            get { return price; }
        }

        public Product(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public static List<Product> GetList()
        {
            return new List<Product>()
            {
                new Product(name: "bread", price: 24)
            };
        }
    }
}
