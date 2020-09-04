using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int PaymentId { get; set; }

        public Payment OrderPayment { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
