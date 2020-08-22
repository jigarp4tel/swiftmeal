using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Models
{
    public class Restaurant
    {

        public int RestaurantID { get; set; }

        [Required]
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }



    }
}
