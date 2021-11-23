using PizzaShop.Calculation;

namespace PizzaShop.PromoCodes
{
    public class FreeDeliveryPromoCode : IPromoCode
    {
        public void Apply(OrderInfo orderInfo)
        {
            orderInfo.Discount += orderInfo.DeliveryPrice;
        }
    }
}