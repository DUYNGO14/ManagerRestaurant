namespace ManagerRestaurant.Application.Users
{
    public record CurrentUser(string userId,string email,IEnumerable<string> Roles)
    {
        public bool IsInRole(string roleName) => Roles.Contains(roleName);
    }
}
