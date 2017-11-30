namespace BusTicketsSystem.App.Core.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using BusTicketsSystem.App.Core.Commands.Contracts;
    using BusTicketsSystem.Data;
    using Microsoft.EntityFrameworkCore;

    public class PrintInfoCommand : ICommand
    {
        private readonly BusTicketSystemContext context;

        public PrintInfoCommand(BusTicketSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            var stations = this.context.Stations
                .Include(s => s.Town)
                .Include(s => s.BusStationDepartureTrips)
                .Include(s => s.BusStationArrivalTrips)
                .ToList();

            var stationId = int.Parse(data[0]);

            var busStation = stations.FirstOrDefault(s => s.StationId == stationId);

            if (busStation == null)
            {
                throw new InvalidOperationException($"There is no bus station with id {stationId}.");
            }

            var sb = new StringBuilder();

            sb.AppendLine($"{busStation.Name}, {busStation.Town.Name}");

            sb.AppendLine("Arrivals:");
            foreach (var arrivingTrip in busStation.BusStationArrivalTrips)
            {
                sb.AppendLine(
                    $"From: {arrivingTrip.OriginBusStation.Town.Name} | Arrive at: {arrivingTrip.ArrivalTime.ToString("hh:mm", CultureInfo.InvariantCulture)} | Status: {arrivingTrip.Status}");
            }

            sb.AppendLine("Departures:");
            foreach (var departingTrip in busStation.BusStationDepartureTrips)
            {
                sb.AppendLine(
                    $"To: {departingTrip.DestinationBusStation.Town.Name} | Depart at: {departingTrip.DepartureTime.ToString("hh:mm", CultureInfo.InvariantCulture)} | Status: {departingTrip.Status}");
            }

            return sb.ToString().Trim();
        }
    }
}
