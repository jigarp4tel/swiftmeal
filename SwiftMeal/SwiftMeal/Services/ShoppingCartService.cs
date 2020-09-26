using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftMeal.Data;
using SwiftMeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly SwiftMealContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpContext _httpContext;

        public ShoppingCartService(SwiftMealContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public Cart GetCartById(int Id)
        {
            var cart = _dbContext.Carts
                .Include(s => s.ItemsList)
                    .ThenInclude(p => p.Product)
                .FirstOrDefault(m => m.Id == Id);
            return cart;
        }

        public Cart GetCartByUserId(string userId)
        {
            var cart = _dbContext.Carts
                .Include(s => s.ItemsList)
                    .ThenInclude(p => p.Product)
                .FirstOrDefault(c => c.UserId == userId);
            return cart;
        }

        public CartItem AddToCart(int productId)
        {
            var userId = _userManager.GetUserId(_httpContext.User);
            Console.WriteLine($"IN ADDTOCART METHOD: {userId}");
            var cart = GetCartByUserId(userId);

            if (cart == null)
            {
                //create new cart for current user.
                cart = CreateCart();
            }

            //after adding the qty field check if the item already exists in the cart. If so increase the qty;            
            //Create shopping cart item
            CartItem cartItem = null;

            if (cart.ItemsList == null)
            {
                cart.ItemsList = new List<CartItem>();
            }

            if (cart.ItemsList.Count() > 0)
            {
                cartItem = cart.ItemsList.FirstOrDefault(p => p.ProductId == productId);
            }

            if (cartItem != null)
            {
                cartItem.Quantity++;
                _dbContext.Update(cartItem);
                _dbContext.SaveChanges();
            }
            else
            {
                cartItem = new CartItem();
                cartItem.ProductId = productId;
                cartItem.CartID = cart.Id;
                cartItem.Quantity = 1;
                _dbContext.Add(cartItem);
                _dbContext.SaveChanges();
            }

            return cartItem;


        }

        private Cart CreateCart()
        {
            var cart = new Cart();
            //set userId from GUID

            var userId = _userManager.GetUserId(_httpContext.User);

            if (userId != null)
            {
                cart.UserId = userId;
            }
            else
            {
                cart.UserId = Guid.NewGuid().ToString();
            }

            _dbContext.Add(cart);
            _dbContext.SaveChanges();

            return cart;
        }


        public Cart UpdateShoppingCartItem(int itemId)
        {
            throw new NotImplementedException();
        }


    }
}
