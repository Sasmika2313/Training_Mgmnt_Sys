using System;
using System.Collections.Generic;
using TMS.Models;
using TMS.Services;
using TMS.Sessions;
using TMS.Payments;

FileService fileService = new FileService();

Dictionary<string, Learner> learners = new Dictionary<string, Learner>();

Course[] courses = {
            new Course("C1", "C SHARP BASICS", 3000m, 15, 30),
            new Course("C2", "JAVA PROGRAMMING", 4000m, 20, 25),
            new Course("C3", "WEB DEVELOPMENT", 3500m, 18, 20),
            new Course("C4", "PYTHON BASICS", 2500m, 12, 30),
            new Course("C5", "DATABASE MNGMNT", 3200m, 15, 20)
            };

List<Enrollment> enrollments = new List<Enrollment>();

EnrollmentService enrollmentService = new EnrollmentService();

FeeCalculator feeCalculator = new FeeCalculator();

List<Payment> payments = new List<Payment>();

PaymentService paymentService = new PaymentService();

int nextPaymentId = 1;

int nextEnrollmentId = 1;
int x = 0;
do
{
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine(
    "Welcome! Choose from the below Options..\n" +
    "1. Display All Courses\n" +
    "2. Search by Course ID or Name\n" +
    "3. Sort by Fee\n" +
    "4. Register Learner\n" +
    "5. Display All Learners\n" +
    "6. Search Learner by ID\n" +
    "7. Remove Learner\n8. Exit\n" +
    "9. Enroll in Course\n" +
    "10. Cancel Enrollment\n"+
    "11. Test Session Types\n"+
    "12. Test Course Summary\n"+
    "13. Make Payment\n"+
    "14. Display Payment History\n");
    Console.WriteLine("15. Save Learner Details to File");
    Console.WriteLine("16. Save Course Details to File");
    Console.WriteLine("17. Save All Details");
    Console.WriteLine("--------------------------------------------");
    if (!int.TryParse(Console.ReadLine(), out x))
    {
        Console.WriteLine("Please enter a valid number!");
        x = 0;
        continue;
    }
    switch (x)
    {
        case 1:
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            foreach (Course course in courses)
            {
                Console.WriteLine($"{course.CId}: {course.Title}\tFee: {course.Fee}\tDuration: {course.DurDay}\tTotal Count: {course.Tot}\t\tEnrolled Count: {course.EnCt}");
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            break;

        case 2:
            Console.Write("Enter Course Id or Name to Search: ");
            string? search = Console.ReadLine();
            foreach (Course ci in courses)
            {
                if (ci.CId.ToUpper() == search?.ToUpper() ||
                    ci.Title.ToUpper() == search?.ToUpper())
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine($"Course Code: {ci.CId}");
                    Console.WriteLine($"Title: {ci.Title}");
                    Console.WriteLine($"Fee: {ci.Fee}");
                    Console.WriteLine($"Duration (Days): {ci.DurDay}");
                    Console.WriteLine($"Total Seats: {ci.Tot}");
                    Console.WriteLine($"Enrolled Count: {ci.EnCt}");
                    Console.WriteLine($"Available Seats: {ci.Tot - ci.EnCt}");
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine();
                }
            }

            break;

        case 3:
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("1. Ascending Order || 2. Descending Order");
            Console.Write("Choose sorting order: ");

            int order = Convert.ToInt32(Console.ReadLine());

            if (order == 1 || order == 2)
            {
                for (int i = 0; i < courses.Length - 1; i++)
                {
                    for (int j = i + 1; j < courses.Length; j++)
                    {
                        bool shouldSwap = order == 1
                            ? courses[i].Fee > courses[j].Fee
                            : courses[i].Fee < courses[j].Fee;

                        if (shouldSwap)
                        {
                            Course temp = courses[i];
                            courses[i] = courses[j];
                            courses[j] = temp;
                        }
                    }
                }

                Console.WriteLine("\nCourses sorted by fee:");
                Console.WriteLine("--------------------------------------------");

                foreach (Course course in courses)
                {
                    Console.WriteLine($"{course.CId} - {course.Title} - Fee: {course.Fee}");
                }

                Console.WriteLine("--------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Invalid sorting option!\n");
                Console.WriteLine("--------------------------------------------");
            }
            break;

        case 4:
            Console.Write("Enter Learner ID: ");
            string learnerId = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(learnerId))
            {
                Console.WriteLine("Learner ID cannot be empty!");
                break;
            }

            if (learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner ID already exists!");
                break;
            }

            Console.Write("Enter Name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty!");
                break;
            }

            Console.Write("Enter Contact Number (10 digits): ");
            string contact = (Console.ReadLine() ?? "").Trim();

            bool validContact = contact.Length == 10;

            for (int i = 0; i < contact.Length; i++)
            {
                if (!char.IsDigit(contact[i]))
                {
                    validContact = false;
                    break;
                }
            }

            if (!validContact)
            {
                Console.WriteLine("Invalid contact number!");
                break;
            }

            Console.Write("Enter Email: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Learner learner = new Learner(
                learnerId, name, contact, email);

            learners.Add(learnerId, learner);

            Console.WriteLine("Learner registered successfully!");
            break;


        case 5:
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered yet.");
                break;
            }

            Console.WriteLine(
                "---------------------------------------------------------------");
            Console.WriteLine("ID\tName\t\tContact\t\tEmail");
            Console.WriteLine(
                "---------------------------------------------------------------");

            foreach (Learner l in learners.Values)
            {
                Console.WriteLine(
                    $"{l.LearnerId}\t{l.Name}\t\t{l.ContactNumber}\t{l.Email}");
            }

            Console.WriteLine(
                "---------------------------------------------------------------");
            break;


        case 6:
            Console.Write("Enter Learner ID to search: ");
            string searchId = (Console.ReadLine() ?? "").Trim();

            if (learners.TryGetValue(searchId, out Learner? foundLearner))
            {
                Console.WriteLine($"Learner ID: {foundLearner.LearnerId}");
                Console.WriteLine($"Name: {foundLearner.Name}");
                Console.WriteLine($"Contact: {foundLearner.ContactNumber}");
                Console.WriteLine($"Email: {foundLearner.Email}");
            }
            else
            {
                Console.WriteLine("Learner not found!");
            }

            break;


        case 7:
            Console.Write("Enter Learner ID to remove: ");
            string removeId = (Console.ReadLine() ?? "").Trim();

            if (learners.Remove(removeId))
            {
                Console.WriteLine("Learner removed successfully!");
            }
            else
            {
                Console.WriteLine("Learner not found!");
            }

            break;


        case 8:
            Console.WriteLine("Exiting application...");
            break;


        case 9:
            Console.Write("Enter Learner ID: ");
            string enrollLearnerId = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter Course Code: ");
            string courseCode = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter Course Start Date (dd/MM/yyyy): ");
            string startInput = Console.ReadLine() ?? "";

            if (!DateTime.TryParseExact(
                startInput, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime courseStartDate))
            {
                Console.WriteLine("Invalid date format!");
                break;
            }

            Console.Write("Session Type (online/classroom): ");
            string sessionType = (Console.ReadLine() ?? "")
                .Trim().ToLowerInvariant();

            if (sessionType != "online" && sessionType != "classroom")
            {
                Console.WriteLine("Invalid session type!");
                break;
            }

            Console.Write("Number of eligible learners enrolling together: ");

            if (!int.TryParse(Console.ReadLine(), out int groupSize)
                || groupSize < 1)
            {
                Console.WriteLine("Invalid group size!");
                break;
            }

            enrollmentService.Enroll(
                enrollLearnerId,
                courseCode,
                courseStartDate,
                sessionType,
                groupSize,
                learners,
                courses,
                enrollments,
                feeCalculator,
                ref nextEnrollmentId);

            break;


        case 10:
            Console.Write("Enter Enrollment ID to cancel: ");

            if (!int.TryParse(Console.ReadLine(), out int cancelId))
            {
                Console.WriteLine("Invalid enrollment ID!");
                break;
            }

            enrollmentService.Cancel(
                cancelId, enrollments, courses);

            break;


        case 11:
            CourseSession online = new OnlineSession(
                "C1", DateTime.Today.AddDays(20),
                new SessionDuration(15));

            CourseSession classroom = new ClassroomSession(
                "C2", DateTime.Today.AddDays(25),
                new SessionDuration(20));

            Console.WriteLine(
                $"Online session charge: ₹{online.CalculateCharge()}");

            Console.WriteLine(
                $"Classroom session charge: ₹{classroom.CalculateCharge()}");

            break;

        case 12:
            Course c = courses[0];

            CourseSummary summary1 = new CourseSummary(
                c.CId, c.Title, c.Fee, c.Tot - c.EnCt);

            CourseSummary summary2 = new CourseSummary(
                c.CId, c.Title, c.Fee, c.Tot - c.EnCt);

            Console.WriteLine(
                $"Summary 1 equals Summary 2: {summary1 == summary2}");

            CourseSummary modified = summary1 with
            {
                AvailableSeats = summary1.AvailableSeats - 1
            };

            Console.WriteLine($"Original seats: {summary1.AvailableSeats}");
            Console.WriteLine($"Modified seats: {modified.AvailableSeats}");

            SessionDuration duration1 = new SessionDuration(15);
            SessionDuration duration2 = duration1;

            duration2.Days = 20;

            Console.WriteLine($"Original duration: {duration1.Days}");
            Console.WriteLine($"Copied duration: {duration2.Days}");

            break;


        case 13:
            Console.Write("Enter Enrollment ID: ");

            if (!int.TryParse(Console.ReadLine(), out int paymentEnrollmentId))
            {
                Console.WriteLine("Invalid enrollment ID!");
                break;
            }

            Enrollment? paymentEnrollment = null;

            foreach (Enrollment e in enrollments)
            {
                if (e.EnrollmentId == paymentEnrollmentId)
                {
                    paymentEnrollment = e;
                    break;
                }
            }

            if (paymentEnrollment == null)
            {
                Console.WriteLine("Enrollment not found!");
                break;
            }

            Console.WriteLine("1. Online Payment");
            Console.WriteLine("2. Classroom Payment");
            Console.Write("Choose processor: ");

            if (!int.TryParse(Console.ReadLine(), out int processorChoice)
                || (processorChoice != 1 && processorChoice != 2))
            {
                Console.WriteLine("Invalid processor choice!");
                break;
            }

            Console.WriteLine("1. Simulate Success");
            Console.WriteLine("2. Simulate Failure");
            Console.Write("Choose payment result: ");

            if (!int.TryParse(Console.ReadLine(), out int resultChoice)
                || (resultChoice != 1 && resultChoice != 2))
            {
                Console.WriteLine("Invalid result choice!");
                break;
            }

            IPaymentProcessor processor;

            if (processorChoice == 1)
            {
                processor = new OnlinePaymentProcessor();
            }
            else
            {
                processor = new ClassroomPaymentProcessor();
            }

            paymentService.MakePayment(
                paymentEnrollment,
                processor,
                resultChoice == 1,
                payments,
                ref nextPaymentId);

            break;


        case 14:
            if (payments.Count == 0)
            {
                Console.WriteLine("No payment attempts recorded.");
                break;
            }

            foreach (Payment p in payments)
            {
                Console.WriteLine(
                    $"Payment ID: {p.PaymentId} | " +
                    $"Enrollment ID: {p.EnrollmentId} | " +
                    $"Amount: ₹{p.Amount:F2} | " +
                    $"Date: {p.PaymentDate} | " +
                    $"Status: {p.Status}");
            }

            break;

        case 15:
            fileService.SaveLearners(learners);
            break;

        case 16:
            fileService.SaveCourses(courses);
            break;

        case 17:
            fileService.SaveLearners(learners);
            fileService.SaveCourses(courses);
            break;

        default:
            Console.WriteLine("Invalid! Please Enter Again..\n");
            break;
    }
} while (x != 8);