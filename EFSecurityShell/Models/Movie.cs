using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFSecurityShell.Models
{
    public class Movie
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Movie Title")]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Director")]
        public string Director { get; set; }

        [Required]
        [Display(Name = "Date Released")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateReleased { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
    }
}