using ManagerRestaurant.Application.Users.Command.AssignUserRole;
using ManagerRestaurant.Application.Users.Command.LoginUser;
using ManagerRestaurant.Application.Users.Command.UnassignUserRole;
using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPost("userRole")]
        [Authorize(Roles = UserRole.Admin)]  
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("singin")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginUser)
        {
            var token = await mediator.Send(loginUser);
            return Ok(new ResponseApi { Success = true, Data = token }); 
        }


    }
}
