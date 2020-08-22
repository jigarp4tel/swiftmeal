using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required, Display(Name = "Product Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        public double Price { get; set; }

        public int CategoryID { get; set; }
        public int RestaurantID { get; set; }

        public Category Category { get; set; }



    }
}
