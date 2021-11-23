using System;
using PizzaShop.Calculation;

namespace PizzaShop.PromoCodes
{
    public class PercentDiscountPromoCode : IPromoCode
    {
        private readonly Decimal _percent;

        public PercentDiscountPromoCode(decimal percent)
        {
            if (_percent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percent));
            }

            _percent = percent;
        }

        public void Apply(OrderInfo orderInfo)
        {
            var discount = orderInfo.TotalPrice * _percent / 100;
            orderInfo.Discount += decimal.Round(discount, 2, MidpointRounding.AwayFromZero);
        }
    }
}