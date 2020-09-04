
using SwiftMeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMeal.Services
{
    public interface IPaymentService
    {
        public Payment SavePayment(Payment payment);
    }
}
