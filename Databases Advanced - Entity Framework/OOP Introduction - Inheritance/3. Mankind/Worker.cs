namespace _3.Mankind
{
    using System;

    public class Worker : Human
    {
        private decimal weekSalary;
        private decimal workHoursPerDay;

        public Worker(string firstName, string secondName, decimal weekSalary, decimal workHoursPerDay)
            : base(firstName, secondName)
        {
            this.WeekSalary = weekSalary;
            this.WorkHoursPerDay = workHoursPerDay;
        }

        protected decimal WeekSalary
        {
            get { return this.weekSalary; }
            set
            {
                if (value <= 10)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
                }
                this.weekSalary = value;
            }
        }

        protected decimal WorkHoursPerDay
        {
            get { return this.workHoursPerDay; }
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
                }
                this.workHoursPerDay = value;
            }
        }

        public decimal GetMoneyPerHour()
        {
            return this.weekSalary / (this.workHoursPerDay * 5);
        }

        public override string ToString()
        {
            return
                $"First Name: {base.FirstName}\nLast Name: {base.LastName}\nWeek Salary: {this.weekSalary:F2}\nHours per day: {this.workHoursPerDay:F2}\nSalary per hour: {this.GetMoneyPerHour():F2}";
        }
    }
}
