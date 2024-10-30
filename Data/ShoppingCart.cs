namespace Ecommerce.Data
{
    public class ShoppingCart
    {
        public List<Cart> CartItems = new List<Cart>();
        public void AddToCart(Product product,int qty)
        {
            var ifexist = CartItems.FirstOrDefault(p=>p.Product.Id == product.Id);
            if(ifexist != null)
            {
                ifexist.Qty += qty;
            }
            else
            {
                CartItems.Add(new Cart {Product = product,Qty = qty });
            }
        }
        public void RemoveItem(int productId)
        {
            CartItems.RemoveAll(p => p.Product.Id == productId);
        }
        public double TotalPrice()
        {
            var totalprice = CartItems.Sum(p => p.Product.Price * p.Qty);
            return totalprice;
        }
    }
}
