using System;
using System.Collections.Generic;
using System.Text;


namespace TMS.Services
{
    public class FeeCalculator
    {
        // Calculate fee without discounts or surcharge
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee)
        {
            if (baseFee <= 0)
                throw new ArgumentException("Fee must be greater than zero.");

            return (baseFee, 0m, baseFee);
        }

        // Calculate fee with discounts and session surcharge
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee, bool earlyBird,
                         int groupSize, string sessionType)
        {
            if (baseFee <= 0)
                throw new ArgumentException("Fee must be greater than zero.");

            if (groupSize < 1)
                throw new ArgumentException("Group size must be at least 1.");

            if (sessionType != "online" &&
                sessionType != "classroom")
                throw new ArgumentException("Invalid session type.");

            decimal discount = 0m;

            if (earlyBird)
                discount += baseFee * 0.10m;

            if (groupSize >= 3)
                discount += baseFee * 0.05m;

            decimal surcharge = sessionType == "online" ? 200m : 800m;

            decimal finalFee = baseFee - discount + surcharge;

            return (baseFee, discount, finalFee);
        }
    }
}
