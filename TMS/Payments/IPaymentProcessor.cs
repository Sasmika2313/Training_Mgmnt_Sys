using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Payments
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount, bool simulateSuccess);
    }
}
