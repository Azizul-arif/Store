using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class RoleManagerController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.OrderBy(x => x.Name).ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = name
            }
            );
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.Errors.Count() > 0)
            {

            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            var result = await _roleManager.Roles.FirstOrDefaultAsync(u => u.Name == name);
            if (result is not null)
            {
                return View(result);
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole identityRole, string name)
        {
            // Use FindByIdAsync to find the role by its ID
            var role = await _roleManager.FindByIdAsync(identityRole.Id);

            if (role is not null)
            {
                
                role.Name = name;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                // Handle the case where the update fails
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(role);
            }

            return NotFound("Role not found.");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string name)
        {
            var result = await _roleManager.Roles.FirstOrDefaultAsync(u => u.Name == name);
            if (result is not null)
            {
                return View(result);
            }
            return null;
        }
        [HttpPost]
        public async Task<IActionResult>Delete(IdentityRole identityRole)
        {
            var role=await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Name == identityRole.Name);
            if(role is not null)
            {
                var result= await _roleManager.DeleteAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }
    }
}
