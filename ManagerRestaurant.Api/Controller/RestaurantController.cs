using ManagerRestaurant.Application.Restaurants.command.Create;
using ManagerRestaurant.Application.Restaurants.command.delete;
using ManagerRestaurant.Application.Restaurants.command.update;
using ManagerRestaurant.Application.Restaurants.command.UploadFile;
using ManagerRestaurant.Application.Restaurants.dto;
using ManagerRestaurant.Application.Restaurants.queries.GetAll;
using ManagerRestaurant.Application.Restaurants.queries.GetAllRestaurantForUser;
using ManagerRestaurant.Application.Restaurants.queries.GetById;
using ManagerRestaurant.Domain.Contants;
using ManagerRestaurant.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerRestaurant.Api.Controller
{
    [Route("api/restaurants")]
    [ApiController]
    [Authorize(Roles = $"{UserRole.Owner},{UserRole.Admin};{UserRole.User}")]
    public class RestaurantController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await mediator.Send(query);
            return Ok(new ResponseApi { Success = true, Data = restaurants });
        }

        [HttpGet("GetAllOwner")]
        [Authorize(Roles = UserRole.Owner)]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllForOnwer([FromQuery] GetRestaurnatForUserQuery query)
        {
            var restaurants = await mediator.Send(query);
            return Ok(new ResponseApi { Success = true, Data = restaurants });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(new ResponseApi { Success = true, Data = restaurant });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            if (!ModelState.IsValid)
            {
                // Tạo một ResponseApi để trả về thông báo lỗi
                var errorMessages = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                var response = new ResponseApi
                {
                    Success = false,
                    Error = errorMessages, // hoặc bạn có thể đưa vào thông điệp cụ thể hơn
                    Data = null
                };

                return BadRequest(response);
            }
            int id = await mediator.Send(createRestaurantCommand);
            return Ok(new ResponseApi { Success = true, Data = id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestuarantCommand(id));
            return Ok(new ResponseApi { Success = true });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return Ok(new ResponseApi { Success = true });
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadFile([FromRoute] int id, IFormFile fileUpload)
        {

            using var stream = fileUpload.OpenReadStream();
            var command = new UploadFileRestaurantLogoCommand()
            {
                RestuarantId = id,
                FilName = Guid.NewGuid().ToString() + fileUpload.FileName,
                File = stream
            };
            await mediator.Send(command);
            return Ok(new ResponseApi { Success = true });
        }
    }
}
