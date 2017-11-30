namespace BusTicketsSystem.App.Core.Commands
{
    using System;
    using System.Linq;
    using BusTicketsSystem.App.Core.Commands.Contracts;
    using BusTicketsSystem.Data;
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class BuyTicketCommand : ICommand
    {
        private readonly BusTicketSystemContext context;

        public BuyTicketCommand(BusTicketSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            var customerId = int.Parse(data[0]);
            var tripId = int.Parse(data[1]);
            var price  = decimal.Parse(data[2]);
            var seat = data[3];
            
            var customer = this.context.Customers.Include(c => c.BankAccount).FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                throw new ArgumentException($"There is no customer with id {customerId}.");
            }

            var trip = this.context.Trips.FirstOrDefault(t => t.TripId == tripId);
            if (trip == null)
            {
                throw new ArgumentException($"There is no trip with id {tripId}.");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Invalid price.");
            }

            if (customer.BankAccount.Balance < price)
            {
                throw new ArgumentException($"Insufficient amount of money for customer {customer.FirstName} {customer.LastName} with bank account number {customer.BankAccount.AccountNumber}");
            }

            var ticket = new Ticket
            {
                Customer = customer,
                Price = price,
                Seat = seat,
                Trip = trip
            };

            customer.BankAccount.Balance -= price;
            this.context.Tickets.Add(ticket);
            this.context.SaveChanges();

            return $"Customer {customer.FirstName} {customer.LastName} bought ticket for trip {tripId} for ${price:F2} on seat {seat}";
        }
    }
}
