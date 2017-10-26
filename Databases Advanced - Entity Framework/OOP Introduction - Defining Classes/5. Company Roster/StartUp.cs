namespace _5.Company_Roster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var employees = new List<Employee>();

            for (int i = 0; i < n; i++)
            {
                var info = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var name = info[0];
                var salary = decimal.Parse(info[1]);
                var position = info[2];
                var department = info[3];

                var employee = new Employee(name, salary, position, department);

                if (info.Length > 4)

                {
                    var ageOrEmail = info[4];
                    if (ageOrEmail.Contains("@"))
                    {
                        employee.Email = ageOrEmail;
                    }
                    else
                    {
                        employee.Age = int.Parse(ageOrEmail);
                    }

                }
                if (info.Length > 5)
                {
                    employee.Age = int.Parse(info[5]);
                }

                employees.Add(employee);
            }

            var result = employees
                .GroupBy(e => e.Department)
                .Select(e => new
                {
                    Department = e.Key,
                    AverageSalary = e.Average(emp => emp.Salary),
                    Employees = e.OrderByDescending(emp => emp.Salary)
                })
                .OrderByDescending(dep => dep.AverageSalary)
                .FirstOrDefault();


            Console.WriteLine($"Highest Average Salary: {result.Department}");

            foreach (var employee in result.Employees)
            {
                Console.WriteLine($"{employee.Name} {employee.Salary:F2} {employee.Email} {employee.Age}");
            }
        }
    }
}
