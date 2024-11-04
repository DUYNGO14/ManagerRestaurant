using Microsoft.AspNetCore.Identity;
using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeder
{
    internal class RestaurantSeeder(RestaurantDbContext context):IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await context.Database.CanConnectAsync())
            {
                //if (!context.Restaurants.Any())
                //{
                //    var restaurant = GetRestaurants();
                //    await context.Restaurants.AddRangeAsync(restaurant);
                //    await context.SaveChangesAsync();
                //}
                if (!context.Roles.Any()) 
                { 
                    var roles = GetRoles();
                    await context.Roles.AddRangeAsync(roles);
                    await context.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                new(UserRole.User){
                    NormalizedName = UserRole.User.ToUpper()
                },
                new(UserRole.Admin){
                    NormalizedName = UserRole.Admin.ToUpper()
                },
                new (UserRole.Owner){
                    NormalizedName = UserRole.Owner.ToUpper()
                },
                ];
            return roles;
        }
        
        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants =
                [
                    new Restaurant(){
                        Name= "McDonal1",
                        Category = "Fast food",
                        Description ="McDonal abcfdgdgdgdgdgdgd",
                        ContactEmail="contact@mcdonal.com",
                        ContactNumber="123445667",
                      
                        Dishes = [
                            new ()
                            {
                                Name ="Chicken",
                                Description="Chicken hot hot1",
                                Price = 76723
                            },
                             new ()
                            {
                                Name ="Banana",
                                Description="Banana hot hot1",
                                Price = 76723
                            }
                             , new ()
                            {
                                Name ="Dog",
                                Description="Dog hot hot1",
                                Price = 76723
                            }
                            ],
                         Address = new Address(){
                            City = "London",
                            Street="Boots 123",
                           
                         }
                    },
                    new Restaurant(){
                        Name= "McDonal2",
                        Category = "Fast food",
                        Description ="McDonal abcfdgdgdgdgdgdgd",
                        ContactEmail="contact@mcdonal.com",
                        ContactNumber="123445667",
                      
                        Address = new Address(){
                            City = "New York",
                            Street="Boots 123",
                       
                        }
                    }
                ];
            return restaurants;
        }
    }
}
