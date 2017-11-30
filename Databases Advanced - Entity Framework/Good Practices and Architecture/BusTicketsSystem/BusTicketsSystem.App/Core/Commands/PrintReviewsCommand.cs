namespace BusTicketsSystem.App.Core.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using BusTicketsSystem.App.Core.Commands.Contracts;
    using BusTicketsSystem.Data;
    using Microsoft.EntityFrameworkCore;

    public class PrintReviewsCommand : ICommand
    {
        private readonly BusTicketSystemContext context;

        public PrintReviewsCommand(BusTicketSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            var companyId = int.Parse(data[0]);

            var company = this.context.Companies.FirstOrDefault(c => c.CompanyId == companyId);
            if (company == null)
            {
                throw new ArgumentException($"There is no bus company with id {companyId}.");
            }

            var reviews = this.context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Company)
                .Where(r => r.CompanyId == companyId)
                .ToList();
            
            var sb = new StringBuilder();
            foreach (var review in reviews)
            {
                sb.AppendLine(
                    $"{review.ReviewId} {review.Grade} {review.DateTimeOfPublishing.ToString("yyyy/mm/dd hh:mm:ss", CultureInfo.InvariantCulture)}");
                sb.AppendLine($"{review.Customer.FirstName} {review.Customer.LastName}: \"{review.Content}\"");
            }

            return sb.ToString().Trim();
        }
    }
}
