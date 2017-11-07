namespace _1._Employees_Full_Information
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees.Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.MiddleName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                    .OrderBy(e => e.EmployeeId)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine(
                        $"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
                }
            }
        }
    }
}
