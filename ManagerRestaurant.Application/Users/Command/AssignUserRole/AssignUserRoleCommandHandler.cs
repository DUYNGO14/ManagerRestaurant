using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;

namespace ManagerRestaurant.Application.Users.Command.AssignUserRole
{
    internal class AssignUserRoleCommandHandler(ILogger<AssemblyLoadEventHandler> logger,
        UserManager<User> userManager,RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        //cấp role cho user
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assign user role {@request}", request);
            var user = await userManager.FindByEmailAsync(request.UserEmail)
                ?? throw new NotFoundException(nameof(User), request.UserEmail); 
            var role = await roleManager.FindByNameAsync(request.Rolename)
                ?? throw new NotFoundException(nameof(IdentityRole), request.Rolename);
            await userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
