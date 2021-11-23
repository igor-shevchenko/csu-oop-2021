using System;
using System.Linq;
using PizzaShop.Calculation;
using PizzaShop.Products;

namespace PizzaShop.Promo
{
    public class FourBigPizzasPromo : IPromo
    {
        private readonly Pizza _smallPizza;

        public FourBigPizzasPromo(Pizza smallPizza)
        {
            if (smallPizza.Size != PizzaSize.Small)
            {
                throw new ArgumentException("Pizza should be small");
            }

            _smallPizza = smallPizza;
        }

        public void Apply(OrderInfo orderInfo)
        {
            var bigPizzas = orderInfo.Items.Where(p => !p.IsPromoApplied)
                .Where(oi => oi.Product.GetType() == typeof(Pizza))
                .Where(oi => ((Pizza) oi.Product).Size == PizzaSize.Largs)
                .Take(4)
                .ToList();

            if (bigPizzas.Count() < 4)
                return;

            bigPizzas.ForEach(oi => oi.IsPromoApplied = true);

            orderInfo.Items.Add(new OrderItem()
            {
                Product = _smallPizza,
                Discount = _smallPizza.Price,
                IsPromoApplied = true
            });
        }
    }
}