using System.Collections.Generic;
using System.Linq;
using PizzaShop.Delivery;
using PizzaShop.Products;
using PizzaShop.Promo;
using PizzaShop.PromoCodes;

namespace PizzaShop.Calculation
{
    public class Cart
    {
        private readonly IDeliveryCalculator _deliveryCalculator;
        private readonly IPromoProvider _promoProvider;

        public Cart(IDeliveryCalculator deliveryCalculator, IPromoProvider promoProvider)
        {
            _deliveryCalculator = deliveryCalculator;
            _promoProvider = promoProvider;
        }

        public OrderInfo GetOrderInfo(List<IProduct> products, DeliveryType deliveryType, IPromoCode promoCode)
        {
            var orderInfo = new OrderInfo
            {
                Items = products.Select(p => new OrderItem {Product = p}).ToList(),
                DeliveryPrice = _deliveryCalculator.Calculate(products,
                    deliveryType)
            };

            promoCode?.Apply(orderInfo);
            _promoProvider.GetActivePromos().ForEach(p => p.Apply(orderInfo));

            return orderInfo;
        }
    }
}