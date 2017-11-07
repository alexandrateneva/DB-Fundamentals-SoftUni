namespace _8._Departments_with_More_Than_5_Employees
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
                var departments = db.Departments
                    .Select(d => new
                    {
                        d.Name,
                        ManagerFirstName = d.Manager.FirstName,
                        ManagerLastName = d.Manager.LastName,
                        EmployeesNamesAndPosition = d.Employees
                        .Select(e => new
                        {
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        }).ToList()
                    })
                    .Where(d => d.EmployeesNamesAndPosition.Count > 5)
                    .OrderBy(d => d.EmployeesNamesAndPosition.Count)
                    .ThenBy(d => d.Name)
                    .ToList();

                foreach (var department in departments)
                {
                    Console.WriteLine($"{department.Name} - {department.ManagerFirstName} {department.ManagerLastName}");

                    foreach (var employee in department.EmployeesNamesAndPosition.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                    {
                        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                    }

                    Console.WriteLine("----------");
                }
            }
        }
    }
}
