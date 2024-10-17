using Ecommerce.Data;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManageUserController(UserManager<ApplicationUser>userManager,IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _webHostEnvironment = environment;
        }
        public async Task <IActionResult>  Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            var userProfile = new UserProfile
            {
                FullName = user.FullName,
                PicPath = user.PicPath
            };
            return View(userProfile);
        }

        [HttpGet]
        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateProfile(UserProfile uSerprofile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (uSerprofile.ProfilePic is null)
            {
                ModelState.AddModelError("", "No Image files were uploaded");
                return View();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "ProfilePic");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            string fileName = Path.GetFileName(uSerprofile.ProfilePic.FileName);
            string fullPath = Path.Combine(imagePath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                uSerprofile.ProfilePic.CopyTo(stream);
            }
            var loggedUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            loggedUser.PicPath = "/ProfilePic/" + fileName;
            loggedUser.FullName = uSerprofile.FullName;
            var result = await _userManager.UpdateAsync(loggedUser);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            if (result.Errors.Count() > 0)
            {

            }

            return View();
        }

    }
}
