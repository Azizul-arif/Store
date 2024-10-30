using Ecommerce.Data;
using Ecommerce.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _cart;
        public ShoppingCartController(ApplicationDbContext context, ShoppingCart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            List<Cart> CartItems = new List<Cart>();
            if(HttpContext.Session.GetObjInSession<Cart>("cart") != null)
            {
                CartItems = HttpContext.Session.GetObjInSession<Cart>("cart");
            }
            ViewBag.TotalPrice =_cart.TotalPrice();
            return View(CartItems);
        }

        public IActionResult AddToCart(int productId ,int? qty)
        {
            var product = _context.Products.Include(u=>u.ProductImages).FirstOrDefault(p => p.Id == productId);
            _cart.AddToCart(product, qty ?? 1);
            HttpContext.Session.SetObjInSession("cart", _cart.CartItems);
            return RedirectToAction("Index", "Home");
        }
    }
}
