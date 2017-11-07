namespace _9._Find_Latest_10_Projects
{
    using System;
    using System.Globalization;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var projects = db.Projects
                    .Select(p => new
                    {
                        p.Name,
                        p.StartDate,
                        p.Description
                    })
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .ToList();

                foreach (var project in projects.OrderBy(p => p.Name))
                {
                    Console.WriteLine($"{project.Name}");
                    Console.WriteLine($"{project.Description}");
                    Console.WriteLine($"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
                    Console.WriteLine();
                }
            }
        }
    }
}
