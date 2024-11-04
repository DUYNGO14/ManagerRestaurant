using MediatR;

namespace ManagerRestaurant.Application.Users.Command.AssignUserRole
{
    public class AssignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string Rolename { get; set; }
    }
}
