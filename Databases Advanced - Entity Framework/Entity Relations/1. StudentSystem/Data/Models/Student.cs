namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
        }

        public Student(string name, string phoneNumber, DateTime registeredOn, DateTime? birthday)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.RegisteredOn = registeredOn;
            this.Birthday = birthday;
        }

        public int StudentId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<StudentCourse> CourseEnrollments { get; set; } = new List<StudentCourse>();

        public ICollection<Homework> HomeworkSubmissions { get; set; } = new List<Homework>();
    }
}
