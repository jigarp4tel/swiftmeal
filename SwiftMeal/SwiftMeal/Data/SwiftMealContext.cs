using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwiftMeal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SwiftMeal.Data
{
    public class SwiftMealContext : IdentityDbContext<ApplicationUser>
    {

        public SwiftMealContext(DbContextOptions<SwiftMealContext> options)
            : base(options)
        {

        }

        public DbSet<SwiftMeal.Models.Product> Product { get; set; }
        public DbSet<SwiftMeal.Models.Category> Category { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }

    }
}
