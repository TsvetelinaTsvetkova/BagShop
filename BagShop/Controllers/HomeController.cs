using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BagShop.Models;
using Microsoft.AspNetCore.Identity;
using BagShop.Data.Models;
using BagShop.Services;
using BagShop.Models.Models.Home;

namespace BagShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IBagService bagService;

        public HomeController(UserManager<User> userManager,IBagService bagService)
        {
            var user = this.userManager;
            this.bagService = bagService;
        }

        public async Task<IActionResult> Index()
        {
            var bags = await this.bagService.ActiveAsync();

            return View(new HomeIndexViewModel
            {
                Bags = bags
            });
        }

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText
            };

            if (model.SearchInBags)
            {
                viewModel.Bags = await this.bagService.FindAsync(model.SearchText);
            }

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
