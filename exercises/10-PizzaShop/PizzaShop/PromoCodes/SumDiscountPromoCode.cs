using System;
using PizzaShop.Calculation;

namespace PizzaShop.PromoCodes
{
    public class SumDiscountPromoCode : IPromoCode
    {
        private decimal _sum;

        public SumDiscountPromoCode(decimal sum)
        {
            _sum = sum;
        }

        public void Apply(OrderInfo orderInfo)
        {
            orderInfo.Discount += Math.Min(_sum, orderInfo.TotalPrice);
        }
    }
}