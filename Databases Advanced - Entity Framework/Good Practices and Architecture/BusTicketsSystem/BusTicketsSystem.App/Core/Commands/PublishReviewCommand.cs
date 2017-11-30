namespace BusTicketsSystem.App.Core.Commands
{
    using System;
    using System.Linq;
    using BusTicketsSystem.App.Core.Commands.Contracts;
    using BusTicketsSystem.Data;
    using BusTicketsSystem.Models;

    public class PublishReviewCommand : ICommand
    {
        private readonly BusTicketSystemContext context;

        public PublishReviewCommand(BusTicketSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            var customerId = int.Parse(data[0]);
            var grade = float.Parse(data[1]);
            var companyName = data[2];
            var content = string.Join(" ", data.Skip(3).ToArray());

            var customer = this.context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                throw new ArgumentException($"There is no customer with id {customerId}.");
            }

            var company = this.context.Companies.FirstOrDefault(c => c.Name == companyName);
            if (company == null)
            {
                throw new ArgumentException($"There is no bus company with name {companyName}.");
            }

            var review = new Review
            {
                Content = content,
                Grade = grade,
                Company = company,
                Customer = customer,
                DateTimeOfPublishing = DateTime.Now
            };

            this.context.Reviews.Add(review);
            this.context.SaveChanges();

            return $"Customer {customer.FirstName} {customer.LastName} published review for company {companyName}.";
        }
    }
}
