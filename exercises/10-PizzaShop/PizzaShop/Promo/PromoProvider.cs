using System.Collections.Generic;
using PizzaShop.Products;

namespace PizzaShop.Promo
{
    public class PromoProvider : IPromoProvider
    {
        public List<IPromo> GetActivePromos()
        {
            return new List<IPromo>
            {
                new FourBigPizzasPromo(
                    new Pizza
                    {
                        Name = "Маргарита",
                        Price = 100,
                        Size = PizzaSize.Small
                    }
                )
            };
        }
    }
}