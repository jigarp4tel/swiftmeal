
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
        private readonly SwiftMealContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ShoppingCartService(SwiftMealContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }
        public Cart GetCartById(int Id)
        {
            var cart = dbContext.Carts
               .Include(s => s.ItemsList).ThenInclude(p => p.CartItemProduct)
                   .FirstOrDefault(m => m.Id == Id);
            return cart;
        }

        public Cart GetCartByUserId(string userId)
        {
            var cart = dbContext.Carts
                .Include(s => s.ItemsList)
                    .ThenInclude(p => p.CartItemProduct)
                .FirstOrDefault(c => c.UserId == userId);
            return cart;
        }

        public Cart UpdateShoppingCartItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem AddToCart(int productId)
        {
            //code to get the current userId 

            var cart = GetCartByUserId("64d5cdbe-01f8-4b87-a5e2-13da6082290a");
            if (cart == null)
            {
                //create new cart for current user. 
                cart = CreateCart();
            }
            //after adding the qty field check if the item already exists in the cart. If so increase the qty;
            //Create shopping cart item
            ShoppingCartItem shoppingCartItem = null;
            if (cart.ItemsList == null)
            {
                cart.ItemsList = new List<ShoppingCartItem>();
            }

            if (cart.ItemsList.Count() > 0)
            {
                shoppingCartItem = cart.ItemsList.FirstOrDefault(i => i.ProductId == productId);
            }

            if (shoppingCartItem != null)
            {
                //cart.ItemsList.FirstOrDefault(i => i == existingItem).Quantity++;
                shoppingCartItem.Quantity++;
                dbContext.Update(shoppingCartItem);
                dbContext.SaveChanges();
            }
            else
            {
                shoppingCartItem = new ShoppingCartItem();
                shoppingCartItem.ProductId = productId;
                shoppingCartItem.ShoppingCartId = cart.Id;
                shoppingCartItem.Quantity = 1;
                //save SCI to database
                dbContext.Add(shoppingCartItem);
                dbContext.SaveChanges();
            }

            return shoppingCartItem;
        }


        private Cart CreateCart()
        {
            var cart = new Cart();

          cart.UserId = Guid.NewGuid().ToString();
     
            dbContext.Add(cart);
            dbContext.SaveChanges();
           
            return cart;
        }




    }
}
