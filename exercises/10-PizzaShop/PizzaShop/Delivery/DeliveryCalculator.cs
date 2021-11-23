using System;
using System.Collections.Generic;
using System.Linq;
using PizzaShop.Products;

namespace PizzaShop.Delivery
{
    public class DeliveryCalculator : IDeliveryCalculator
    {
        public decimal Calculate(List<IProduct> products, DeliveryType deliveryType)
        {
            switch (deliveryType)
            {
                case DeliveryType.Delivery:
                    var hasPizza = products.OfType<Pizza>().Any();
                    if (!hasPizza)
                        throw new NoPizzaException();
                    var sum = products.Sum(p => p.Price);
                    if (sum < 1000)
                        return 150;
                    return 0;
                case DeliveryType.Pickup:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deliveryType), deliveryType, null);
            }
        }
    }
}