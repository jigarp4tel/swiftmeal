using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwiftMeal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SwiftMeal.Data
{
    public class SwiftMealContext : IdentityDbContext<ApplicationUser>
    {
        public SwiftMealContext (DbContextOptions<SwiftMealContext> options)
            : base(options)
        {
        }

        public DbSet<SwiftMeal.Models.Product> Product { get; set; }
        public DbSet<SwiftMeal.Models.Category> Category { get; set; }
    }
}
