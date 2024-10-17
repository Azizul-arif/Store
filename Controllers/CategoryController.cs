using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        public ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category categorydata)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(categorydata);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }

        public IActionResult Update(int categorydata)
        {
            var categoryObj = _db.Categories.FirstOrDefault(u => u.Id == categorydata);
            if (categoryObj is not null)
            {
                return View(categoryObj);
            }
            return View("Error", "Index");
        }

        [HttpPost]
        public IActionResult Update(Category categorydata)
        {
            var categoryObj = _db.Categories.FirstOrDefault(u => u.Id == categorydata.Id);
            if (categoryObj is not null)
            {
                categoryObj.CategoryName = categorydata.CategoryName;
                categoryObj.ParentID = categorydata.ParentID;
                _db.Categories.Update(categoryObj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }

        public IActionResult Delete(int? categorydata)
        {
            var categoryObj = _db.Categories.FirstOrDefault(u => u.Id == categorydata);
            if (categoryObj is not null)
            {
                return View(categoryObj);
            }
            return View("Error", "Index");
        }

        [HttpPost]
        public IActionResult Delete(Category? categorydata)
        {
            var categoryObj = _db.Categories.FirstOrDefault(u => u.Id == categorydata.Id);
            if (categoryObj is not null)
            {
                _db.Categories.Remove(categoryObj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }

    }
}
