using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ManagerRestaurant.Domain.Entities;

namespace ManagerRestaurant.Infrastructure.Persistence
{
    public class RestaurantDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.OwnsOne(r=>r.Address); 

                    entity.HasMany(r=>r.Dishes)
                    .WithOne()
                    .HasForeignKey(d=>d.RestaurantId)
                    .HasConstraintName("FK_Restaurant_Dish")
                    //.OnDelete(DeleteBehavior.Cascade)
                    ;
                });
           
            modelBuilder.Entity<Dish>().HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .HasMany(o => o.OwnedRestaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId);
        }
    }
}
