using System;
using System.Collections.Generic;
using System.IO;
using TMS.Models;

namespace TMS.Services
{
    public class FileService
    {
        private readonly string folderPath = "Data";

        public FileService()
        {
            Directory.CreateDirectory(folderPath);
        }

        public void SaveLearners(Dictionary<string, Learner> learners)
        {
            string path = Path.Combine(folderPath, "Learners.txt");

            using StreamWriter writer = new StreamWriter(path);

            foreach (Learner learner in learners.Values)
            {
                writer.WriteLine($"Learner ID: {learner.LearnerId}");
                writer.WriteLine($"Name: {learner.Name}");
                writer.WriteLine($"Contact: {learner.ContactNumber}");
                writer.WriteLine($"Email: {learner.Email}");
                writer.WriteLine("-----------------------------------");
            }

            Console.WriteLine("Learner details saved to file.");
        }

        public void SaveCourses(Course[] courses)
        {
            string path = Path.Combine(folderPath, "Courses.txt");

            using StreamWriter writer = new StreamWriter(path);

            foreach (Course course in courses)
            {
                writer.WriteLine($"Course ID: {course.CId}");
                writer.WriteLine($"Title: {course.Title}");
                writer.WriteLine($"Fee: {course.Fee}");
                writer.WriteLine($"Duration: {course.DurDay} days");
                writer.WriteLine($"Total Seats: {course.Tot}");
                writer.WriteLine($"Enrolled Count: {course.EnCt}");
                writer.WriteLine($"Available Seats: {course.Tot - course.EnCt}");
                writer.WriteLine("-----------------------------------");
            }

            Console.WriteLine("Course details saved to file.");
        }
    }
}