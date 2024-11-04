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

namespace ManagerRestaurant.Application.Users.Queries.GetById
{
    public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger,IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<GetUserByIdQuery, IdentityUser>
    {
        public async Task<IdentityUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
          //  var user = userContext.GetCurrentUser();
            logger.LogInformation("Getting user : {UserId}, with {@Request}", request!.UserId, request);
            var dbuser = await userStore.FindByIdAsync(request.UserId, cancellationToken);
            if (dbuser == null) 
            {
                throw new NotFoundException(nameof(User), request.UserId); 
            }
            return dbuser;
        }
    }
}
