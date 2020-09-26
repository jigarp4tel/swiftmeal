using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwiftMeal.Data;
using SwiftMeal.Models;
using SwiftMeal.Services;

namespace SwiftMeal.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly SwiftMealContext appDbContext;
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(SwiftMealContext appDbContext, IShoppingCartService shoppingCartService, UserManager<ApplicationUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            var userId = _userManager.GetUserId(User);
            var cart = shoppingCartService.GetCartByUserId(userId);
            Console.WriteLine($"IN CART CONTROLLER: {userId}");
            return View(cart);
        }

        public IActionResult AddToCart(int productId)
        {
            shoppingCartService.AddToCart(productId);
            return RedirectToAction("Cart");
        }


    }
}
