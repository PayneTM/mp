using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WannaTravel.Models.DbEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
        public string Salt { get; set; }
    }
}
