using System.Collections.Generic;

namespace PizzaShop.Promo
{
    public interface IPromoProvider
    {
        List<IPromo> GetActivePromos();
    }
}