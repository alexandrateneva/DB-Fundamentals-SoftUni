namespace P01_StudentSystem
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data;
    using P01_StudentSystem.Data.Models;
    using P01_StudentSystem.Enums;

    public class StartUp
    {
        public static void Main()
        {
            var context = new StudentSystemContext();

            ResetDatabase(context);
        }

        private static void ResetDatabase(StudentSystemContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(StudentSystemContext context)
        {
            var students = new[]
            {
                new Student("Ivan", "0895631805", DateTime.Parse("10.10.2017"), DateTime.Parse("13.06.1989")),
                new Student("Pesho", "0989194290", DateTime.Parse("08.11.2017"), null),
                new Student("Maria", "0787207519", DateTime.Parse("24.09.2017"), null)
            };

            context.Students.AddRange(students);

            var courses = new[]
            {
                new Course("Object Oriented Programming",
                    "This module goes over the differences between Object Oriented Programming (OOP), Managed Languages and why use C# for OOP.",
                     DateTime.Parse("15.10.2017"), DateTime.Parse("15.11.2017"), 150.00m),
                new Course("Code Reflection and Information",
                    "This session provides an overview of controlling programmatic flow and manipulating types and strings.",
                    DateTime.Parse("16.11.2017"), DateTime.Parse("15.12.2017"), 150.00m),
                new Course("Interacting with the File System",
                    "This session provides an overview about interacting with the file system and leveraging web services.",
                    DateTime.Parse("16.12.2017"), DateTime.Parse("15.01.2018"), 150.00m)
            };

            context.Courses.AddRange(courses);

            var resources = new[]
            {
                new Resource("OOP - Managed Languages and C#","https://mva.microsoft.com/en-us/training-courses/programming-in-c-jump-start-14254?l=MqbQvzSfB_1500115888", ResourceType.Video, courses[0].CourseId),
                new Resource("Reflection in C# Tutorial","https://www.codeproject.com/Articles/17269/Reflection-in-C-Tutorial", ResourceType.Document, courses[1].CourseId),
                new Resource("Interacting with the File System","https://channel9.msdn.com/Series/Programming-in-C-Jump-Start/Programming-in-C-07-Interacting-with-the-File-System-and-Leveraging-Web-Services", ResourceType.Video, courses[2].CourseId),
            };

            context.Resources.AddRange(resources);

            var homeworks = new[]
            {
                new Homework("Object-oriented programming (OOP) exercises", ContentType.Application, DateTime.Parse("16.11.2017 10:15:00"),students[2].StudentId, courses[0].CourseId ),
                new Homework("Reflection in C# exercises", ContentType.Zip, DateTime.Parse("18.11.2017 21:35:10"),students[2].StudentId, courses[1].CourseId ),
                new Homework("File System exercises", ContentType.Zip, DateTime.Parse("20.11.2017 15:18:49"),students[1].StudentId, courses[2].CourseId )
            };

            context.HomeworkSubmissions.AddRange(homeworks);

            var studentCourses = new[]
            {
                new StudentCourse(students[2].StudentId, courses[0].CourseId),
                new StudentCourse(students[2].StudentId, courses[1].CourseId),
                new StudentCourse(students[1].StudentId, courses[2].CourseId),
                new StudentCourse(students[0].StudentId, courses[2].CourseId)
            };

            context.StudentCourses.AddRange(studentCourses);

            context.SaveChanges();
        }
    }
}
