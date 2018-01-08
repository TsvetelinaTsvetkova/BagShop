namespace BagShop.Areas.Admin.Controllers
{
    using BagShop.Areas.Admin.Models.Identity;
    using BagShop.Data;
    using BagShop.Data.Models;
    using BagShop.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class IdentityController : Controller
    {
        private readonly BagShopDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityController(BagShopDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult All()
        {
            var users = this.db
                .Users
                .OrderBy(u => u.Email)
                .Select(u => new ListUserViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email
                })
                .ToList();

            return View(users);
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);

            return View(new UserWithRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles
            });
        }

        public IActionResult AddToRole(string id)
        {
            var rolesSelectListItems = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(rolesSelectListItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (user == null || !roleExists)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, role);

            TempData["SuccessMessage"] = $"User{user.Email} added to {role} role";
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Destroy(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.DeleteAsync(user);

            TempData["SuccessMessage"] = $"User {user.Email}deleted!";
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new IdentityChangePasswordViewModel
            {
                Email = user.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, IdentityChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"Password changed for user {model.Email}";
                return RedirectToAction(nameof(All));
            }
            else
            {
                this.AddModelErrors(result);
                return View(model);
            }
        }

        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
