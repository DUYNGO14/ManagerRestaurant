using ManagerRestaurant.Domain.Respository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagerRestaurant.Infrastructure.Respository
{
    public class UserRepository(UserManager<IdentityUser> userManager) : IUserRepository
    {
        public async Task<List<IdentityUser>> GetAllUser(string id)
        {
            var users = await userManager.Users.ToListAsync();
            var nonAdminUsers = new List<IdentityUser>();

            foreach (var user in users)
            {
                if (user.Id != id) { nonAdminUsers.Add(user); }
            }
            return nonAdminUsers;
        }
    }
}
