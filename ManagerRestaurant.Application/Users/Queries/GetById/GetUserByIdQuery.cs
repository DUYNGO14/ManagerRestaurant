using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerRestaurant.Application.Users.Queries.GetById
{
    public class GetUserByIdQuery(string id) : IRequest<IdentityUser>
    {
        public string UserId { get; set; } = id;
    }
}
