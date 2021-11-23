using PizzaShop.Calculation;

namespace PizzaShop.PromoCodes
{
    public interface IPromoCode
    {
        void Apply(OrderInfo orderInfo);
    }
}