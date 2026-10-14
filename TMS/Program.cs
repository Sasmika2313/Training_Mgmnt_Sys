using System;
using System.Collections.Generic;
using TMS.Models;

Dictionary<string, Learner> learners =
    new Dictionary<string, Learner>();
Course[] courses = {
            new Course("C1", "C SHARP BASICS", 3000m, 15, 30),
            new Course("C2", "JAVA PROGRAMMING", 4000m, 20, 25),
            new Course("C3", "WEB DEVELOPMENT", 3500m, 18, 20),
            new Course("C4", "PYTHON BASICS", 2500m, 12, 30),
            new Course("C5", "DATABASE MNGMNT", 3200m, 15, 20)
            };
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
    "7. Remove Learner\n" +
    "8. Exit");
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
            foreach (Course c in courses)
            {
                if (c.CId.ToUpper() == search?.ToUpper() ||
                    c.Title.ToUpper() == search?.ToUpper())
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine($"Course Code: {c.CId}");
                    Console.WriteLine($"Title: {c.Title}");
                    Console.WriteLine($"Fee: {c.Fee}");
                    Console.WriteLine($"Duration (Days): {c.DurDay}");
                    Console.WriteLine($"Total Seats: {c.Tot}");
                    Console.WriteLine($"Enrolled Count: {c.EnCt}");
                    Console.WriteLine($"Available Seats: {c.Tot - c.EnCt}");
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

        default:
            Console.WriteLine("Invalid! Please Enter Again..\n");
            break;
    }
} while (x != 8);