using BagShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace BagShop.Controllers
{
    using BagShop.Data;
    using BagShop.Data.Models;
    using BagShop.Infrastructure.Extensions;
    using BagShop.Models.Models.ShoppingCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;

        private readonly BagShopDbContext db;

        private readonly UserManager<User> userManager;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager, BagShopDbContext db, UserManager<User> userManager)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemsWithDetails = this.GetCartItems(items);

            return View(itemsWithDetails);
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.AddToCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.RemoveFromCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }

        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemsWithDetails = this.GetCartItems(items);

            var order = new Order
            {
                UserId = this.userManager.GetUserId((User)),
                TotalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity)
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderItem
                {
                    BagId = item.BagId,
                    BagPrice = item.Price,
                    Quantity = item.Quantity
                });
            }

            this.db.Add(order);
            this.db.SaveChanges();

            shoppingCartManager.Clear(shoppingCartId);

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        private List<CartItemViewModel> GetCartItems(IEnumerable<CartItem> items)
        {
            var itemIds = items.Select(i => i.BagId);

            var itemsWithDetails = this.db
                 .Bags
                 .Where(b => itemIds.Contains(b.Id))
                 .Select(b => new CartItemViewModel
                 {
                     BagId = b.Id,
                     Title = b.Title,
                     Price = b.Price
                 })
                 .ToList();

            var itemQuantities = items.ToDictionary(i => i.BagId, i => i.Quantity);

            itemsWithDetails.ForEach(i => i.Quantity = itemQuantities[i.BagId]);

            return itemsWithDetails;
        }
    }
}
