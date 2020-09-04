
using SwiftMeal.Data;
using SwiftMeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly SwiftMealContext DbContext;

        public PaymentService(SwiftMealContext  dbContext)
        {
            this.DbContext = dbContext;
        }
        public Payment SavePayment(Payment payment)
        {
            DbContext.Add(payment);
            DbContext.SaveChanges();
            return payment;            
        }
    }
}
