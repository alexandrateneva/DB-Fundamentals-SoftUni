namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class Station
    {
        public int StationId { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Trip> BusStationArrivalTrips { get; set; } = new HashSet<Trip>();

        public ICollection<Trip> BusStationDepartureTrips { get; set; } = new HashSet<Trip>();
    }
}