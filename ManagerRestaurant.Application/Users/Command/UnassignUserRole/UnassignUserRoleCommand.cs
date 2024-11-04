using MediatR;

namespace ManagerRestaurant.Application.Users.Command.UnassignUserRole
{
    public class UnassignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
    }
}
