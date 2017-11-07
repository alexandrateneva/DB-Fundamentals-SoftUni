namespace _11._Find_Employees_by_First_Name
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            const string StartOfTheName = "Sa";

            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.Salary
                    })
                    .Where(e => e.FirstName.StartsWith(StartOfTheName))
                    .ToList();

                foreach (var employee in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
                }
            }
        }
    }
}
