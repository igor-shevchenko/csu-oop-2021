using System;

namespace PizzaShop.Products
{
    public class Drink : IProduct
    {
        public string Name { get; set; }
        public Decimal Price { get; set; }
    }
}