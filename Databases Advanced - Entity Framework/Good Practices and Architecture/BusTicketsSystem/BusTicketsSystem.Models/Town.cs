namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public int TownId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();

        public ICollection<Station> Stations { get; set; } = new HashSet<Station>();
    }
}
