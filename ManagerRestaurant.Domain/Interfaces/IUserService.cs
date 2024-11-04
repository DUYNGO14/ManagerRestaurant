namespace ManagerRestaurant.Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);
    }
}
