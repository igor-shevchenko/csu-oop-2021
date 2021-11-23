using System.Linq;
using PizzaShop.Calculation;
using PizzaShop.Products;

namespace PizzaShop.PromoCodes
{
    public class FreePizzePromoCode : IPromoCode
    {
        private readonly Pizza _pizza;

        public FreePizzePromoCode(Pizza pizza)
        {
            _pizza = pizza;
        }

        public void Apply(OrderInfo orderInfo)
        {
            var orderItem = orderInfo.Items.Where(p => !p.IsPromoApplied)
                .Where(oi => oi.Product.GetType() == typeof(Pizza))
                .FirstOrDefault(oi => oi.Product.Name == _pizza.Name && ((Pizza)oi.Product).Size == _pizza.Size);
            if (orderItem == null)
                return;

            orderItem.IsPromoApplied = true;
            orderItem.Discount = orderItem.FinalPrice;
        }
    }
}