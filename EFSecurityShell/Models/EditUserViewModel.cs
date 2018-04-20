using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFSecurityShell.Models
{
    public class EditUserViewModel
    {

        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public IEnumerable<SelectListItem> RolesList{ get; set; }
    }
}