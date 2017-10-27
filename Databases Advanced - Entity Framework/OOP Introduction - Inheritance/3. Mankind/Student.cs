namespace _3.Mankind
{
    using System;
    using System.Text.RegularExpressions;

    public class Student : Human
    {
        private string facultyNumber;

        public Student(string firstName, string secondName, string facultyNumber)
            : base(firstName, secondName)
        {
            this.FacultyNumber = facultyNumber;
        }

        protected string FacultyNumber
        {
            get { return this.facultyNumber; }
            set
            {
                var regex = new Regex("^([A-Za-z0-9]{5,10})$");
                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("Invalid faculty number!");
                }
                this.facultyNumber = value;
            }
        }

        public override string ToString()
        {
            return $"First Name: {base.FirstName}\nLast Name: {base.LastName}\nFaculty number: {this.facultyNumber}";
        }
    }
}