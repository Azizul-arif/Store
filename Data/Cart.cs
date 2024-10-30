namespace Ecommerce.Data
{
    public class Cart
    {
        public Product Product { get; set; } = new Product();
        public int Qty { get; set; }
    }
}
