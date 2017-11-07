namespace _2._Employees_with_Salary_Over_50_000
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
                var employees = db.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.Salary
                    })
                    .Where(s => s.Salary > 50000)
                    .OrderBy(e => e.FirstName)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.FirstName);
                }
            }
        }
    }
}
