using ManagerRestaurant.Application.Users.Queries.GetById;
using ManagerRestaurant.Application.Users.Queries.GetUserByToken;
using ManagerRestaurant.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerRestaurant.Api.Controller
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            var user = await mediator.Send(new GetUserByIdQuery(userId));
            return Ok(new ResponseApi { Data = user,Success=true });
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await mediator.Send(new GetUserQuery());
            return Ok(new ResponseApi { Data = user, Success = true });
        }

       
     
    }
}
