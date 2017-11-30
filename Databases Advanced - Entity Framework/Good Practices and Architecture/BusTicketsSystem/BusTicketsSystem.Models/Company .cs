namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }
        
        [Range(1, 10, ErrorMessage = "Raiting can be a floating-point number in range [1, 10].")]
        public float Raiting { get; set; }

        public ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
