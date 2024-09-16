using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;

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
            ViewBag.ProductName = new SelectList(_db.Products, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductImages productImages, List<IFormFile> images)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProductNAme"] = new SelectList(_db.Products, "Id", "Name");
                return View(productImages);
            }
            if (images is null || images.Count == 0)
            {
                ModelState.AddModelError("", "No Image files were uploaded");
                ViewData["ProductName"] = new SelectList(_db.Products, "Id", "Name");
                return View(productImages);
            }

            string imagePath = Path.Combine(_environment.WebRootPath, "Images");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            foreach (var image in images)
            {
                string fileName = Path.GetFileName(image.FileName);
                string fullPath = Path.Combine(imagePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                productImages.ImagePath = "/Images/" + fileName;
                _db.Add(productImages);
                _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
