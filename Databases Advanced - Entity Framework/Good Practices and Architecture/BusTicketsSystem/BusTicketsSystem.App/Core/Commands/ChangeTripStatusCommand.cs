namespace BusTicketsSystem.App.Core.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;
    using BusTicketsSystem.App.Core.Commands.Contracts;
    using Microsoft.EntityFrameworkCore;
    using BusTicketsSystem.Data;
    using BusTicketsSystem.Models;

    public class ChangeTripStatusCommand : ICommand
    {
        private readonly BusTicketSystemContext context;

        public ChangeTripStatusCommand(BusTicketSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            var tripId = int.Parse(data[0]);
            var statusName = data[1];

            var trip = this.context.Trips
                .Include(t => t.DestinationBusStation).ThenInclude(ds => ds.Town)
                .Include(t => t.OriginBusStation).ThenInclude(ds => ds.Town)
                .Include(t => t.Tickets)
                .FirstOrDefault(t => t.TripId == tripId);

            if (trip == null)
            {
                throw new ArgumentException($"There is no trip with id {tripId}.");
            }

            if (!Enum.TryParse(statusName, out Status newStatus))
            {
                throw new ArgumentException("Invalid status!");
            }

            var oldStatus = trip.Status;

            if (newStatus == oldStatus)
            {
                throw new InvalidOperationException($"Status is already {newStatus}");
            }

            trip.Status = newStatus;
            this.context.SaveChanges();

            var result =
                $"Trip from {trip.OriginBusStation.Town.Name} to {trip.DestinationBusStation.Town.Name} on {trip.DepartureTime.ToString("yyyy/mm/dd hh:mm:ss", CultureInfo.InvariantCulture)}" +
                Environment.NewLine + $"Status changed from {oldStatus} to {newStatus}";

            if (newStatus == Status.Arrived)
            {
                return result + $"On {trip.ArrivalTime.ToString("yyyy/mm/dd hh:mm:ss", CultureInfo.InvariantCulture)} - {trip.Tickets.Count} passengers arrived at {trip.DestinationBusStation.Town.Name} from {trip.OriginBusStation.Town.Name}";
            }

            return result;
        }
    }
}
