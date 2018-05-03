using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFSecurityShell.Models
{
    public class Review
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Movie Title")]
        public int MovieID { get; set; }
        [Display(Name = "Movie Title")]
        public virtual Movie Movie { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string ReviewName { get; set; }

        [Required]
        [Display(Name = "Rate this Movie on a scale of One to Ten")]
        public Score Score { get; set; }

        [Required]
        [Display(Name = "Review")]
        [DataType(DataType.MultilineText)]
        public string ReviewContent { get; set; }

        [Display(Name = "Typical Genres you watch / Review")]
        public List<EnumModel> CheckBoxGenre { get; set; }

        [Display(Name = "Favorite Genre")]
        public Genre FavoriteGenre { get; set; }
    }
}