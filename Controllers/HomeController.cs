using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ecommerce.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _environment;
        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment environment)
		{
			_logger = logger;
            _db = db;
            _environment = environment;
        }

		public IActionResult Index()
		{
			var product=_db.Products.Include(u=>u.ProductImages).ToList();
			return View(product);
		}
		public IActionResult Details(int? productID)
		{
			var data = _db.Products.Include(u=>u.ProductImages).FirstOrDefault(u => u.Id == productID);
			if(data is not null)
			{
				return View(data);
			}
			return Content("No data found");

		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
