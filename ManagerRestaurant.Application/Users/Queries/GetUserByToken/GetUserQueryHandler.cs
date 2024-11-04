using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerRestaurant.Application.Users.Queries.GetUserByToken
{
    public class GetUserQueryHandler(ILogger<GetUserQueryHandler> logger, IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<GetUserQuery, IdentityUser>
    {
        public async Task<IdentityUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Getting user : {UserId}", user!.userId  );           
            var dbuser = await userStore.FindByIdAsync(user.userId, cancellationToken);
            if (dbuser == null) { throw new NotFoundException(nameof(User), user.userId); }

            return dbuser;
        }
    }
}
