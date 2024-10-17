using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var productList = _db.Products.Include(u => u.Category);
            return View(productList.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product productData)
        {
            ViewBag.CategoryList = new SelectList(_db.Products, "Id", "CategoryName");
            if (ModelState.IsValid)
            {
                _db.Products.Add(productData);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Home");

        }
        public IActionResult Update(int productId)
        {
            ViewBag.CategoryList = new SelectList(_db.Products, "Id", "CategoryName");
            var productData = _db.Products.FirstOrDefault(u => u.Id == productId);
            if (productData is not null)
            {
                return View(productData);
            }
            return View("Error", "Home");
        }
        [HttpPost]
        public IActionResult Update(Product? productObj)
        {
            var productData = _db.Products.FirstOrDefault(u => u.Id == productObj.Id);
            if (productData is not null)
            {
                productData.Name = productData.Name;
                productData.Description = productData.Description;
                productData.Price = productData.Price;
                productData.CategoryId = productData.CategoryId;
                _db.Products.Update(productData);
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Home");
        }
        public IActionResult Delete(int productId)
        {
            ViewBag.CategoryList = new SelectList(_db.Products, "Id", "CategoryName");
            var productData = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (productData is not null)
            {
                return View(productData);
            }
            return View("Error", "Home");
        }
        [HttpPost]
        public IActionResult Delete(Product productObj)
        {
            var productData = _db.Products.FirstOrDefault(u => u.Id == productObj.Id);
                {
                if (productData is not null)
                {
                    _db.Remove(productObj);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View("Error", "Home");
            }
        }
    }
}
