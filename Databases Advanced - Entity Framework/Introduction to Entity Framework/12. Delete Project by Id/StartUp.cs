namespace _12._Delete_Project_by_Id
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            const int IdToDelete = 2;

            using (var db = new SoftUniContext())
            {
                var projectToDelete = db.Projects.Find(IdToDelete);

                var employeesProjectsToDelete = db.EmployeesProjects.Where(ep => ep.ProjectId == IdToDelete).ToList();

                db.EmployeesProjects.RemoveRange(employeesProjectsToDelete);

                db.Projects.Remove(projectToDelete);

                db.SaveChanges();

                foreach (var project in db.Projects.Select(p => new { p.Name }).Take(10))
                {
                    Console.WriteLine(project.Name);
                }
            }
        }
    }
}
