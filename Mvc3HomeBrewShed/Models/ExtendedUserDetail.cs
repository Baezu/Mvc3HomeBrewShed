using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Mvc3HomeBrewShed.Models
{
    public class ExtendedUserDetail
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ExtendedUserDetailContext : DbContext
    {
        public DbSet<ExtendedUserDetail> ExtendedUserDetails { get; set; }
    }
}