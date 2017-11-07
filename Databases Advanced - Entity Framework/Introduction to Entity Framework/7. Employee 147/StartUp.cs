namespace _7._Employee_147
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
                var employee = db.Employees
                    .Include(e => e.EmployeesProjects)
                    .ThenInclude(ep => ep.Project)
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        Projects = e.EmployeesProjects.Select(p => p.Project.Name).ToList()
                    })
                    .SingleOrDefault(e => e.EmployeeId == 147);

                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var project in employee.Projects.OrderBy(p => p))
                {
                    Console.WriteLine(project);
                }
            }
        }
    }
}
