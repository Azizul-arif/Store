using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public ProductImagesController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var data = _db.ProductImages.Include(u => u.Product).ThenInclude(u => u.Category);
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.ProductName = new SelectList(_db.Products,"Id", "Name");
            return View();
        }
    }
}
