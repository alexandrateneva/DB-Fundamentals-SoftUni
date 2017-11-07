namespace _13._Remove_Towns
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
                Console.Write("Please, enter a valid town name: ");
                var townName = Console.ReadLine();

                var employees = db.Employees
                    .Include(e => e.Address)
                    .ThenInclude(a => a.Town)
                    .Where(e => e.Address.Town.Name == townName)
                    .ToList();

                employees.Select(e => e.AddressId = null);

                var addresses = db.Addresses
                    .Where(a => a.Town.Name == townName)
                    .ToList();

                var correctFormOfBe = (employees.Count == 1) ? "was" : "were";
                Console.WriteLine($"{addresses.Count} address in {townName} {correctFormOfBe} deleted.");

                db.Addresses.RemoveRange(addresses);

                var town = db.Towns.FirstOrDefault(t => t.Name == townName);

                if (town == null)
                {
                    Console.WriteLine("There is no such town in the database.");
                }
                else
                {
                    db.Towns.Remove(town);
                    Console.WriteLine("The town was successfully removed.");

                    db.SaveChanges();
                }
            }
        }
    }
}
