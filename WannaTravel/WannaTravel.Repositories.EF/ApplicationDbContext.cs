using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WannaTravel.Models.DbEntities;

namespace WannaTravel.Repositories.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }


        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Restaurant>()
                .HasMany(i => i.Comments)
                .WithRequired(y => y.Restaurant);

            modelBuilder.Entity<Comment>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Comment>()
                .HasRequired(i => i.Restaurant);

            base.OnModelCreating(modelBuilder);
        }
    }
}
