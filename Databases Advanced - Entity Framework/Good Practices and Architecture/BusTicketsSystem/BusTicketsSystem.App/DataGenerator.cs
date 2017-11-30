namespace BusTicketsSystem.App
{
    using System;
    using BusTicketsSystem.Data;
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public static class DataGenerator
    {
        public static void ResetDatabase(BusTicketSystemContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        public static void Seed(BusTicketSystemContext context)
        {
            var towns = new[]
            {
                new Town {Name = "Sofia", Country = "Bulgaria"},
                new Town {Name = "Berlin", Country = "Germany"},
                new Town {Name = "Paris", Country = "France"},
                new Town {Name = "Madrid", Country = "Spain"},
                new Town {Name = "Amsterdam", Country = "Holand"}
            };
            context.Towns.AddRange(towns);

            var busStations = new[]
            {
                new Station {Name = "Sofia Bus Station", Town = towns[0]},
                new Station {Name = "Berlin Bus Station", Town = towns[1]},
                new Station {Name = "Paris Bus Station", Town = towns[2]},
                new Station {Name = "Madrid Bus Station", Town = towns[3]},
                new Station {Name = "Amsterdam Bus Station", Town = towns[4]}
            };
            context.Stations.AddRange(busStations);

            var busCompanies = new[]
            {
                new Company {Name = "EtapAdress", Nationality = "Bugarien", Raiting = 6.5f},
                new Company {Name = "Eurolines", Nationality = "French", Raiting = 7.5f},
                new Company {Name = "SchnellBus", Nationality = "German", Raiting = 9.5f},
                new Company {Name = "EspanaRapido", Nationality = "Spanish", Raiting = 6.5f},
                new Company {Name = "OnZeitBus", Nationality = "Dutch", Raiting = 8.5f}
            };
            context.Companies.AddRange(busCompanies);

            var customers = new[]
            {
                new Customer
                {
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    DateOfBirth = DateTime.Parse("14-11-1992"),
                    Gender = Gender.Male,
                    HomeTown = towns[0]
                },
                new Customer
                {
                    FirstName = "Gerald",
                    LastName = "Hass",
                    DateOfBirth = DateTime.Parse("10-04-1987"),
                    Gender = Gender.Male,
                    HomeTown = towns[1]
                },
                new Customer
                {
                    FirstName = "Jose",
                    LastName = "Cortez",
                    DateOfBirth = DateTime.Parse("26-06-1978"),
                    Gender = Gender.Male,
                    HomeTown = towns[2]
                },
                new Customer
                {
                    FirstName = "Eleonore",
                    LastName = "Beavis",
                    DateOfBirth = DateTime.Parse("11-10-1987"),
                    Gender = Gender.Female,
                    HomeTown = towns[3]
                },
                new Customer
                {
                    FirstName = "Erich",
                    LastName = "Fulhert",
                    DateOfBirth = DateTime.Parse("06-02-1969"),
                    Gender = Gender.Male,
                    HomeTown = towns[4]
                }
            };
            context.Customers.AddRange(customers);

            var bankAccounts = new[]
            {
                new BankAccount{ AccountNumber = "8267GTZ928JO1", Balance = 1450.00m, Customer = customers[0] },
                new BankAccount{ AccountNumber = "6272GT72519HPQZ", Balance = 500.00m, Customer = customers[1] },
                new BankAccount{ AccountNumber = "GSJAGI173027JS60", Customer = customers[2] },
                new BankAccount{ AccountNumber = "162066GAOZZJKA", Balance = 100.00m, Customer = customers[3] },
                new BankAccount{ AccountNumber = "BSOWVSG172628J", Balance = 2050.00m, Customer = customers[4] }
            };
            context.BankAccounts.AddRange(bankAccounts);

            var trips = new[]
            {
                new Trip
                {
                    Company = busCompanies[0],
                    OriginBusStation = busStations[3],
                    DestinationBusStation = busStations[4],
                    DepartureTime = DateTime.Parse("29-11-2017 6:30"),
                    ArrivalTime = DateTime.Parse("30-11-2017 23:30"),
                    Status = Status.Departed
                },
                new Trip
                {
                    Company = busCompanies[1],
                    OriginBusStation = busStations[0],
                    DestinationBusStation = busStations[1],
                    DepartureTime = DateTime.Parse("30-11-2017 12:30"),
                    ArrivalTime = DateTime.Parse("01-11-2017 22:00"),
                    Status = Status.Cancelled
                },
                new Trip
                {
                    Company = busCompanies[2],
                    OriginBusStation = busStations[2],
                    DestinationBusStation = busStations[3],
                    DepartureTime = DateTime.Parse("01-12-2017 17:30"),
                    ArrivalTime = DateTime.Parse("03-12-2017 09:00"),
                    Status = Status.Delayed
                },
                new Trip
                {
                    Company = busCompanies[2],
                    OriginBusStation = busStations[2],
                    DestinationBusStation = busStations[0],
                    DepartureTime = DateTime.Parse("30-11-2017 13:30"),
                    ArrivalTime = DateTime.Parse("01-12-2017 21:30"),
                    Status = Status.Arrived
                },
                new Trip
                {
                    Company = busCompanies[4],
                    OriginBusStation = busStations[0],
                    DestinationBusStation = busStations[4],
                    DepartureTime = DateTime.Parse("04-12-2017 22:30"),
                    ArrivalTime = DateTime.Parse("06-12-2017 14:00"),
                    Status = Status.Departed
                },
            };
            context.Trips.AddRange(trips);

            var tickets = new[]
            {
                new Ticket {Customer = customers[0], Price = 150.00m, Seat = "A4", Trip = trips[0]},
                new Ticket {Customer = customers[0], Price = 210.00m, Seat = "C2", Trip = trips[1]},
                new Ticket {Customer = customers[1], Price = 265.50m,Seat = "F2", Trip = trips[0]},
                new Ticket {Customer = customers[2], Price = 180.00m, Seat = "D5", Trip = trips[1]},
                new Ticket {Customer = customers[2], Price = 120.00m, Seat = "H3", Trip = trips[3]},
                new Ticket {Customer = customers[3], Price = 185.50m, Seat = "L2", Trip = trips[2]}
            };
            context.Tickets.AddRange(tickets);

            var reviews = new[]
            {
                new Review
                {
                    Content = "Excellent trip! Look forward to travel again.",
                    Grade = 8.5f,
                    Company = busCompanies[0],
                    Customer = customers[0],
                    DateTimeOfPublishing = DateTime.Now
                },
                new Review
                {
                    Content = "Very polite staff.",
                    Grade = 8.5f,
                    Company = busCompanies[1],
                    Customer = customers[0],
                    DateTimeOfPublishing = DateTime.Now
                },
                new Review
                {
                    Content = "The driver was careful.",
                    Grade = 8.0f,
                    Company = busCompanies[1],
                    Customer = customers[1],
                    DateTimeOfPublishing = DateTime.Now
                },
                new Review
                {
                    Content = "Would recommend it but the driver needs to stop smoking while driving.",
                    Grade = 4.0f,
                    Company = busCompanies[2],
                    Customer = customers[3],
                    DateTimeOfPublishing = DateTime.Now
                },
                new Review
                {
                    Content = "It was a pleasant trip.",
                    Grade = 8.5f,
                    Company = busCompanies[3],
                    Customer = customers[4],
                    DateTimeOfPublishing = DateTime.Now
                }
            };
            context.Reviews.AddRange(reviews);

            context.SaveChanges();
        }
    }
}

