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
        public IActionResult Cart()
        {
            // use the current user's ID to get the cart          
            var cart = shoppingCartService.GetCartByUserId("64d5cdbe-01f8-4b87-a5e2-13da6082290a");
            return View(cart);
 
            // return View("~/Views/Home/index.cshtml");
        }

        public IActionResult AddtoCart(int productId)
        {
            shoppingCartService.AddToCart(productId);
            return RedirectToAction("Cart");

        }


    }
}
