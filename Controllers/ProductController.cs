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
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }

        public IActionResult Update(int? productID)
        {
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "CategoryName");
            var productData = _db.Products.FirstOrDefault(u => u.Id == productID);
            if (productData is not null)
            {
                return View(productData);
            }
            return View("Error","Home");
        }
        [HttpPost]
        public IActionResult Update(Product productData)
        {
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "CategoryName");
            var productObj= _db.Products.FirstOrDefault(u=>u.Id == productData.Id);
            if (productObj is not null)
            {
                productObj.Name = productData.Name;
                productObj.Description = productData.Description;
                productObj.CategoryId = productData.CategoryId;
                productObj.Price = productData.Price;

                _db.Products.Update(productObj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error","Home");

        }
        public IActionResult Delete(int? productID)
        {
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "CategoryName");
            var productData = _db.Products.FirstOrDefault(u => u.Id == productID);
            if (productData is not null)
            {
                return View(productData);
            }
            return View("Error", "Home");
        }
        [HttpPost]
        public IActionResult Delete(Product? productData)
        {
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "CategoryName");
            var productObj = _db.Products.FirstOrDefault(u => u.Id == productData.Id);
            if (productData is not null)
            {
                _db.Products.Remove(productObj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
                
            }
            return View("Error", "Home");
        }
    }
}
