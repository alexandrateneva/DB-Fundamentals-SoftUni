namespace _5._Employees_and_Projects
{
    using System;
    using System.Globalization;
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
                    .Include(e => e.Manager)
                    .Include(e => e.EmployeesProjects)
                    .ThenInclude(ep => ep.Project)
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        ManagerFirstName = e.Manager.FirstName,
                        ManagerLastName = e.Manager.LastName,
                        Projects = e.EmployeesProjects
                        .Select(p => new
                        {
                            ProjectName = p.Project.Name,
                            p.Project.StartDate,
                            p.Project.EndDate
                        }).ToList()
                    })
                    .Where(e => e.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                    .OrderBy(e => e.EmployeeId)
                    .Take(30)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                    foreach (var project in employee.Projects)
                    {
                        var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        var endDate = (!project.EndDate.HasValue)
                            ? "not finished"
                            : project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                        Console.WriteLine($"--{project.ProjectName} - {startDate} - {endDate}");
                    }
                }
            }
        }
    }
}
