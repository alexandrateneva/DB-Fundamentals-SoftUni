namespace _6._Addresses_by_Town
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
                var addresses = db.Addresses
                    .Select(adr => new
                    {
                        adr.AddressText,
                        TownName = adr.Town.Name,
                        EmployeesCount = adr.Employees.Count
                    })
                    .OrderByDescending(a => a.EmployeesCount)
                    .ThenBy(a => a.TownName)
                    .ThenBy(a => a.AddressText)
                    .Take(10)
                    .ToList();

                foreach (var address in addresses)
                {
                    Console.WriteLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
                }
            }
        }
    }
}
