using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models
{
    public class Enrollment
    {
        public PaymentStatus PaymentStatus { get; set; }
        public int EnrollmentId { get; set; }
        public string LearnerId { get; set; }
        public string CourseCode { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal BaseFee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalFee { get; set; }
        public EnrollmentStatus Status { get; set; }

        public Enrollment(int enrollmentId, string learnerId,
                          string courseCode, DateTime enrollmentDate,
                          decimal baseFee, decimal discountAmount,
                          decimal finalFee)
        {
            EnrollmentId = enrollmentId;
            LearnerId = learnerId;
            CourseCode = courseCode;
            EnrollmentDate = enrollmentDate;
            BaseFee = baseFee;
            DiscountAmount = discountAmount;
            FinalFee = finalFee;
            Status = EnrollmentStatus.Active;
            PaymentStatus = PaymentStatus.Pending;

        }
    }
}