using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Infrastructure.Persistence;
using ManagerRestaurant.Domain.Respository;
using ManagerRestaurant.Infrastructure.Respository;
using ManagerRestaurant.Domain.Interfaces;
using ManagerRestaurant.Infrastructure.Storage;
using Restaurants.Infrastructure.Seeder;


namespace ManagerRestaurant.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("RestaurantDb");
            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddIdentityApiEndpoints<User>()
                     .AddRoles<IdentityRole>()
                    // .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
                     .AddEntityFrameworkStores<RestaurantDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IRestaurantsRespository, RestaurantRespository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IBlodStoregeService, BlodStoregeService>();
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            
          
        }
    }
}
