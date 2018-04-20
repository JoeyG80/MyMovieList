using EFSecurityShell.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFSecurityShell.DAL
{
    public class BookASeatDbContext: DbContext
    {
        public DbSet<Flight> Flights { get; set; }
    }
}