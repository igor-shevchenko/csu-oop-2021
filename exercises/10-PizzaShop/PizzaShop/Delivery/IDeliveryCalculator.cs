using System.Collections.Generic;
using PizzaShop.Products;

namespace PizzaShop.Delivery
{
    public interface IDeliveryCalculator
    {
        decimal Calculate(List<IProduct> products, DeliveryType deliveryType);
    }
}