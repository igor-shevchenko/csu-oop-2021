using System;

namespace PizzaShop.Products
{
    public class Pizza : IProduct
    {
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public PizzaSize Size { get; set; }
    }
}