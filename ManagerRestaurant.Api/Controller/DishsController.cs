using ManagerRestaurant.Application.Dishs.command.create;
using ManagerRestaurant.Application.Dishs.command.delete.deleteAll;
using ManagerRestaurant.Application.Dishs.command.delete.deleteById;
using ManagerRestaurant.Application.Dishs.command.update;
using ManagerRestaurant.Application.Dishs.dto;
using ManagerRestaurant.Application.Dishs.queries.getAll;
using ManagerRestaurant.Application.Dishs.queries.getById;
using ManagerRestaurant.Domain.Entities;
using ManagerRestaurant.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerRestaurant.Api.Controller
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await mediator.Send(command);
            return Ok(new ResponseApi { Success = true,Data = dishId});
            // return CreatedAtAction(nameof(GetDishByIdForRestaurant), new { restaurantId, dishId }, null);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishForRestaurantQuery(restaurantId));
            return Ok(new ResponseApi { Success = true,Data= dishes });
        }
        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(dishId, restaurantId));
            return Ok(new ResponseApi { Success = true, Data = dish });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteAllDishForRestaurantCommand(restaurantId));
            return Ok(new ResponseApi { Success = true });
        }
        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await mediator.Send(new DeleteDishByIdForRestaurantCommand(dishId, restaurantId));
            return Ok(new ResponseApi { Success = true });
        }

        [HttpPatch("{dishId}")]
        public async Task<IActionResult> UpadteDishByIdForRetaurant([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishForRestaurantCommand command)
        {
            command.DishId = dishId;
            command.RestaurantId = restaurantId;
            await mediator.Send(command);
            return Ok(new ResponseApi { Success = true });
        }
    }
}
