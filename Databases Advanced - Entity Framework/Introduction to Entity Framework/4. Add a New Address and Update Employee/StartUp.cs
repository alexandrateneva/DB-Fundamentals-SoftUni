namespace _4._Add_a_New_Address_and_Update_Employee
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var address = new Address()
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                var employee = db.Employees.FirstOrDefault(e => e.LastName == "Nakov");

                employee.Address = address;

                db.SaveChanges();

                var employees = db.Employees
                    .Select(e => new
                    {
                        e.AddressId,
                        e.Address.AddressText
                    })
                    .OrderByDescending(e => e.AddressId)
                    .Take(10)
                    .ToList();

                foreach (var empl in employees)
                {
                    Console.WriteLine(empl.AddressText);
                }
            }
        }
    }
}
