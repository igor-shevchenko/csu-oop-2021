using System;
using System.Collections.Generic;
using PizzaShop.Calculation;
using PizzaShop.Delivery;
using PizzaShop.Products;
using PizzaShop.Promo;

namespace PizzaShop
{
    class Program
    {
        static void Main(string[] args)
        {
            var deliveryCalculator = new DeliveryCalculator();
            var promoProvider = new PromoProvider();
            var cart = new Cart(deliveryCalculator, promoProvider);

            var orderInfo = cart.GetOrderInfo(
                new List<IProduct>
                {
                    new Pizza
                    {
                        Name = "Pizza 1",
                        Price = 100,
                        Size = PizzaSize.Largs
                    },
                    new Pizza
                    {
                        Name = "Pizza 2",
                        Price = 100,
                        Size = PizzaSize.Largs
                    },
                    new Pizza
                    {
                        Name = "Pizza 3",
                        Price = 100,
                        Size = PizzaSize.Largs
                    },
                    new Pizza
                    {
                        Name = "Pizza 4",
                        Price = 1000,
                        Size = PizzaSize.Largs
                    }
                }, DeliveryType.Delivery, null);
            Console.WriteLine(orderInfo.TotalPrice);
            orderInfo.Items.ForEach(item => Console.WriteLine(item.Product.Name));
        }
    }
}
