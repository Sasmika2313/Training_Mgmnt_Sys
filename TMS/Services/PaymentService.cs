using System;
using System.Collections.Generic;
using System.Text;

using TMS.Models;
using TMS.Payments;

namespace TMS.Services
{
    public class PaymentService
    {
        public void MakePayment(
            Enrollment enrollment,
            IPaymentProcessor processor,
            bool simulateSuccess,
            List<Payment> payments,
            ref int nextPaymentId)
        {
            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine(
                    "Payment rejected: enrollment is cancelled.");
                return;
            }

            bool success = processor.ProcessPayment(
                enrollment.FinalFee, simulateSuccess);

            PaymentStatus status = success
                ? PaymentStatus.Successful
                : PaymentStatus.Failed;

            Payment payment = new Payment(
                nextPaymentId,
                enrollment.EnrollmentId,
                enrollment.FinalFee,
                DateTime.Now,
                status);

            payments.Add(payment);
            nextPaymentId++;

            if (success)
            {
                enrollment.PaymentStatus = PaymentStatus.Successful;
                Console.WriteLine("Payment successful!");
            }
            else
            {
                Console.WriteLine("Payment failed. Attempt recorded.");
            }

            Console.WriteLine($"Payment ID: {payment.PaymentId}");
            Console.WriteLine($"Amount: ₹{payment.Amount:F2}");
            Console.WriteLine($"Status: {payment.Status}");
        }
    }
}