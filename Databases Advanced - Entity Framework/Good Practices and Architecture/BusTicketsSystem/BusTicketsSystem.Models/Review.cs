namespace BusTicketsSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int ReviewId { get; set; }

        public string Content { get; set; }

        [Range(1, 10, ErrorMessage = "Raiting can be a floating-point number in range [1, 10].")]
        public float Grade { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime DateTimeOfPublishing { get; set; }
    }
}
