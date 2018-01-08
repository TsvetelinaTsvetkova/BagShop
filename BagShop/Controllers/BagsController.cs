using BagShop.Data.Models;
using BagShop.Models.Bags;
using BagShop.Models.Models.Bags;
using BagShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BagShop.Controllers
{
    public class BagsController : Controller
    {
        private readonly IBagService bagService;
        private readonly UserManager<User> userManager;

        public BagsController(IBagService bagService,
            UserManager<User> userManager)
        {
            this.bagService = bagService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Add(AddBagViewModel bagModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bagModel);
            }

            this.bagService.Create(
                bagModel.ImageUrl,
                bagModel.Title,
                bagModel.Color,
                bagModel.Price,
                bagModel.Description,
                bagModel.Quantity,
                this.userManager.GetUserId(User));

            return RedirectToAction("All");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All(int page=1)
        {
            var bags = this.bagService.All(page);

            return View(bags);
        }

        public IActionResult Details(int? id)
        {
            if (this.bagService.BagExists(id))
            {
               BagFullDetailsModel bagFullDetailsModel = this.bagService.GetBagFullDetails(id);

                return View(bagFullDetailsModel);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id != null && this.bagService.BagExists(id))
            {
                EditBagModel editBagModel = this.bagService.GetBagCompleteEditInformation(id.Value);

                return View(editBagModel);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Edit(EditBagModel editBagModel)
        {
            if (this.ModelState.IsValid && this.bagService.BagExists(editBagModel.BagId))
            {
                this.bagService.EditBag(editBagModel);

                return RedirectToAction("Index", "Home");
            }

            return View(editBagModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id != null && this.bagService.BagExists(id))
            {
                EditBagModel editBagModel = this.bagService.GetBagCompleteEditInformation(id.Value);

                return View(editBagModel);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Destroy(int? bagId)
        {
            if (bagId != null && this.bagService.BagExists(bagId))
            {
                this.bagService.DeleteBag(bagId.Value);

                return RedirectToAction("Index", "Home");
            }

            return BadRequest();
        }
    }
}
