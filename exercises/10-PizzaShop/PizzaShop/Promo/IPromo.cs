using PizzaShop.Calculation;

namespace PizzaShop.Promo
{
    public interface IPromo
    {
        void Apply(OrderInfo orderInfo);
    }
}