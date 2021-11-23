using PizzaShop.Products;

namespace PizzaShop.Calculation
{
    public class OrderItem
    {
        public IProduct Product { get; set; }
        public decimal Discount { get; set; }
        public bool IsPromoApplied { get; set; }

        public decimal InitialPrice => Product.Price;
        public decimal FinalPrice => InitialPrice - Discount;
    }
}