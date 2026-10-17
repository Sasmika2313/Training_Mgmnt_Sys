using System;
using System.Collections.Generic;
using System.Text;


namespace TMS.Payments
{
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount, bool simulateSuccess)
        {
            Console.WriteLine("Processing online payment...");

            if (amount <= 0)
                return false;

            return simulateSuccess;
        }
    }
}
