using SwiftMeal.Data;
using SwiftMeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Services
{
    public class OrderService:IOrderService
    {
        private readonly SwiftMealContext DbContext;

        public OrderService(SwiftMealContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public Order SaveOrder(Order order)
        {
            DbContext.Add(order);
            DbContext.SaveChanges();
            return order;
        }

        public OrderItem SaveOrderItem(OrderItem orderItem)
        {
            DbContext.Add(orderItem);
            DbContext.SaveChanges();
            return orderItem;
        }
    }
}
