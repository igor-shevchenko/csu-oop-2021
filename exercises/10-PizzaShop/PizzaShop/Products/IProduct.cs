using System;

namespace PizzaShop.Products
{
    public interface IProduct
    {
        string Name { get; set; }
        Decimal Price { get; set; }
    }
}