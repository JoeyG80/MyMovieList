using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFSecurityShell.Models
{
    public class Flight
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Originating Airport Code")]
        public string OriginatingAirport { get; set; }
        [Required]
        [Display(Name = "Destination Airport Code")]
        public string DestinationAirport { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Departure Date/Time")]
        public DateTime DepartureDate { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name = "Arrival Date/Time")]
        public DateTime ArrivalDate { get; set; }
        [Required]
        [Display(Name = "Duration (Minutes)")]
        public int FlightDurationInMinutes { get; set; }
        [Display(Name = "Flight Description")]
        [Required]
        [MaxLength(100)]
        public string FlightDescription { get; set; }

    }
}