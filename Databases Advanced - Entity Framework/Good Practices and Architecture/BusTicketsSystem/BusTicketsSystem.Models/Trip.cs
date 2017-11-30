namespace BusTicketsSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Trip
    {
        public int TripId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Status Status { get; set; }

        public int OriginBusStationId { get; set; }
        public Station OriginBusStation { get; set; }

        public int DestinationBusStationId { get; set; }
        public Station DestinationBusStation { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
