using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFSecurityShell.Models
{
    public class EnumModel
    {
        public int ID { get; set; }
        public Genre Genre { get; set; }
        public bool IsSelected { get; set; }
    }
}