using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Payments
{
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(
            decimal amount, bool simulateSuccess)
        {
            Console.WriteLine("Processing classroom payment...");

            if (amount <= 0)
                return false;

            return simulateSuccess;
        }
    }
}
