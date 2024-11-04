using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ManagerRestaurant.Application.Users.Queries.GetUserByToken
{
    public class GetUserQuery : IRequest<IdentityUser>
    {
    }
}
