namespace _3._Employees_from_Research_and_Development
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Include(emp => emp.Departments)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Salary,
                        DepartmentName = e.Department.Name
                    })
                    .Where(dep => dep.DepartmentName == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
                }
            }
        }
    }
}
