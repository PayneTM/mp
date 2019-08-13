using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WannaTravel.Models.DbEntities
{
    public class Restaurant
    {
        //public Restaurant()
        //{
        //    Comments = new HashSet<Comment>();
        //}

        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isClaimed { get; set; }

        public string WebsiteUrl { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string About { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}