namespace _10._Increase_Salaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            const decimal IncreaseRate = 1.12m;

            var departmentsToIncreaseSalaries = new List<string>()
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                     .Where(e => departmentsToIncreaseSalaries.Contains(e.Department.Name))
                     .ToList();

                foreach (var employee in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    employee.Salary = employee.Salary * IncreaseRate;

                    db.SaveChanges();

                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
                }
            }
        }
    }
}
