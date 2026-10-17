using System;
using System.Collections.Generic;
using System.Text;
using TMS.Models;

namespace TMS.Services
{
    public class EnrollmentService
    {
        public void Enroll(
            string learnerId,
            string courseCode,
            DateTime courseStartDate,
            string sessionType,
            int groupSize,
            Dictionary<string, Learner> learners,
            Course[] courses,
            List<Enrollment> enrollments,
            FeeCalculator calculator,
            ref int nextEnrollmentId)
        {
            if (!learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner does not exist!");
                return;
            }

            Course? selectedCourse = null;

            foreach (Course c in courses)
            {
                if (string.Equals(c.CId, courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedCourse = c;
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Course does not exist!");
                return;
            }

            foreach (Enrollment e in enrollments)
            {
                if (e.LearnerId.Equals(
                        learnerId, StringComparison.OrdinalIgnoreCase) &&
                    e.CourseCode.Equals(
                        selectedCourse.CId, StringComparison.OrdinalIgnoreCase) &&
                    e.Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine("Already actively enrolled!");
                    return;
                }
            }

            if (selectedCourse.EnCt >= selectedCourse.Tot)
            {
                Console.WriteLine("No seats available!");
                return;
            }

            DateTime enrollmentDate = DateTime.Today;

            bool earlyBird =
                (courseStartDate - enrollmentDate).TotalDays >= 14;

            (decimal BaseFee, decimal DiscountAmount, decimal FinalFee) fee;

            try
            {
                fee = calculator.CalculateFee(
                    selectedCourse.Fee, earlyBird, groupSize,
                    sessionType.ToLowerInvariant());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Enrollment enrollment = new Enrollment(
                nextEnrollmentId,
                learnerId,
                selectedCourse.CId,
                enrollmentDate,
                fee.BaseFee,
                fee.DiscountAmount,
                fee.FinalFee);

            enrollments.Add(enrollment);
            selectedCourse.EnCt++;
            nextEnrollmentId++;

            Console.WriteLine("\nEnrollment successful!");
            Console.WriteLine($"Enrollment ID: {enrollment.EnrollmentId}");
            Console.WriteLine($"Learner ID: {enrollment.LearnerId}");
            Console.WriteLine($"Course: {selectedCourse.Title}");
            Console.WriteLine($"Base Fee: ₹{fee.BaseFee:F2}");
            Console.WriteLine($"Discount: ₹{fee.DiscountAmount:F2}");
            Console.WriteLine($"Final Fee: ₹{fee.FinalFee:F2}");
            Console.WriteLine($"Status: {enrollment.Status}");
            Console.WriteLine($"Available Seats: {selectedCourse.Tot - selectedCourse.EnCt}");
        }

        public void Cancel(
            int enrollmentId,
            List<Enrollment> enrollments,
            Course[] courses)
        {
            Enrollment? selectedEnrollment = null;

            foreach (Enrollment e in enrollments)
            {
                if (e.EnrollmentId == enrollmentId)
                {
                    selectedEnrollment = e;
                    break;
                }
            }

            if (selectedEnrollment == null)
            {
                Console.WriteLine("Enrollment not found!");
                return;
            }

            if (selectedEnrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Enrollment is already cancelled!");
                return;
            }

            if (selectedEnrollment.Status != EnrollmentStatus.Active)
            {
                Console.WriteLine("Only active enrollments can be cancelled!");
                return;
            }

            foreach (Course c in courses)
            {
                if (c.CId.Equals(
                    selectedEnrollment.CourseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (c.EnCt > 0)
                        c.EnCt--;

                    break;
                }
            }

            selectedEnrollment.Status = EnrollmentStatus.Cancelled;

            Console.WriteLine(
                $"Enrollment {enrollmentId} cancelled successfully.");
        }
    }
}
