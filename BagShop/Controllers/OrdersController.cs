using BagShop.Data;
using BagShop.Data.Models;
using BagShop.Models.Models.Orders;
using BagShop.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BagShopDbContext db;
        private readonly UserManager<User> userManager;

        public OrdersController(BagShopDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var orders = this.db.Orders.ToList();

            List<OrderViewModel> model = new List<OrderViewModel>();

            if (orders == null)
            {
                return View(model);
            }

            foreach (var order in orders)
            {
                User user = await this.userManager.FindByIdAsync(order.UserId);
                var userName = user.UserName;
                var viewModel = new OrderViewModel
                {
                    Id = order.Id,
                    TotalPrice = order.TotalPrice,
                    UserName = userName
                };
                model.Add(viewModel);
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string orderId)
        {
            var viewModel = new OrderDetailsViewModel();

            var order = this.db.Orders.FirstOrDefault(x => x.Id.ToString() == orderId);

            var user = await this.userManager.FindByIdAsync(order.UserId);

            viewModel.UserName = user.UserName;
            viewModel.FullPrice = order.TotalPrice;

            List<OrderItem> orderItems = this.db.OrderItem.Where(x => x.OrderId.ToString() == orderId).ToList();

            if (orderItems == null)
            {
                return View(viewModel);
            }

            List<OrderProductDetailsViewModel> orderProductDetails = new List<OrderProductDetailsViewModel>();

            foreach (var product in orderItems)
            {
                var bag = this.db.Bags.FirstOrDefault(x => x.Id == product.BagId);
                var bagOrder = this.db.OrderItem.FirstOrDefault(x => x.BagId == product.BagId);

                if (bag != null)
                {
                    var productViewModel = new OrderProductDetailsViewModel
                    {
                        ProductName = bag.Title,
                        ProductPrice = bagOrder.BagPrice,

                        Quantity = product.Quantity
                    };
                    orderProductDetails.Add(productViewModel);
                }
            }
            viewModel.Products = orderProductDetails;

            viewModel.OrderId = orderId.ToString();

            return View(viewModel);
        }
    }
}