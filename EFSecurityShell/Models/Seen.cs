using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFSecurityShell.Models
{
    public class Seen
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Movie Title")]
        public int MovieID { get; set; }
        [Display(Name = "Movie Title")]
        public virtual Movie Movie { get; set; }

        [Display(Name = "Date Seen")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateSeen { get; set; }

        [Display(Name = "Rate this Movie on a scale of One to Ten")]
        public Score Score { get; set; }
    }
}