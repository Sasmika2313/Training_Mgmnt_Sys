using System;
using TMS.Models;

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
    Console.WriteLine("Welcome! Choose from the below Options..\n1. Display All Courses\n2. Search by Course ID or Name\n3. Sort by Fee\n4. Register");
    Console.WriteLine("--------------------------------------------");
    x = Convert.ToInt32(Console.ReadLine());
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
        default:
            Console.WriteLine("Invalid! Please Enter Again..\n");
            break;
    }
} while (x != 4);