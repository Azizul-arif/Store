using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ColorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ColorController(ApplicationDbContext db)
        {
            _db= db;
        }

        public IActionResult Index()
        {
            var colordata = _db.Colors.ToList();
            return View(colordata);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Color colordata)
        {
            if(ModelState.IsValid)
            {
                _db.Colors.Add(colordata);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }

        public IActionResult Update(int ColorID)
        {
            var colorobj = _db.Colors.FirstOrDefault(color => color.Id == ColorID);
            if(colorobj is not null)
            {
                return View(colorobj);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public IActionResult Update(Color color)
        {
            var colorObj= _db.Colors.FirstOrDefault(u=>u.Id==color.Id);
            if(colorObj is not null)
            {
                colorObj.Name= color.Name;
                _db.Colors.Update(colorObj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }
        public IActionResult Delete(int colorID)
        {
            var Id = _db.Colors.FirstOrDefault(u=>u.Id==colorID);
            if(Id is not null)
            {
                return View(Id);
            }
            return View("Error","Home");
        }
        [HttpPost]
        public IActionResult Delete(Color colordata)
        {
            var colordataObj = _db.Colors.FirstOrDefault(u=>u.Id==colordata.Id);
            if(colordataObj is not null)
            {
                _db.Colors.Remove(colordataObj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error", "Index");
        }
    }
}
