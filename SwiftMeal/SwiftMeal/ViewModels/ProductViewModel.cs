using Microsoft.AspNetCore.Http;
using SwiftMeal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.ViewModels
{
    public class ProductViewModel
    {

        [Required, StringLength(100), Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required, Display(Name = "Product Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public double Price { get; set; }

        public int CategoryID { get; set; }
        public int RestaurantID { get; set; }

        public Category Category { get; set; }
    }
}
