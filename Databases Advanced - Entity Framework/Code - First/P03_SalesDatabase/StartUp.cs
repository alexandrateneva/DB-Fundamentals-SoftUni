namespace P03_SalesDatabase
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data;
    using P03_SalesDatabase.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            var context = new SalesContext();

            ResetDatabase(context);
        }

        private static void ResetDatabase(SalesContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(SalesContext context)
        {
            var customers = new[]
            {
                new Customer("Ivan", "ivan123@gmail.com", "2685H8NSQLX"),
                new Customer("Petyr", "pesho1987@gmail.com", "829HOTYP89"),
                new Customer("Maria", "mimi_01@gmail.com", "HETAB6QW816")
            };

            context.Customers.AddRange(customers);

            var products = new[]
            {
                new Product("Apple", 12.476, 1.60m),
                new Product("Orange", 17.267, 2.70m),
                new Product("Banana", 10.682, 1.80m, "Bananas are fairly nutritious, and contain high amounts of fiber and antioxidants.")
            };

            context.Products.AddRange(products);

            var stores = new[]
            {
                new Store("Mandarin"),
                new Store("Fresh"),
                new Store("Plodchetata")
            };

            context.Stores.AddRange(stores);

            var sales = new[]
            {
                new Sale(DateTime.Parse("10.10.2017"), products[0].ProductId, customers[2].CustomerId, stores[1].StoreId),
                new Sale(products[1].ProductId, customers[0].CustomerId, stores[2].StoreId),
                new Sale(DateTime.Parse("09.04.2017"), products[2].ProductId, customers[1].CustomerId, stores[0].StoreId)
            };

            context.Sales.AddRange(sales);

            context.SaveChanges();
        }
    }
}
