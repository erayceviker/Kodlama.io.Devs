using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetByIdUser;
using Application.Features.Users.Queries.GetListByDynamicUser;
using Application.Features.Users.Queries.GetListUser;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteUserCommand deleteUserCommand)
        {
             await Mediator.Send(deleteUserCommand);

            return NoContent();
        }

        [HttpPut]
        [Route("Update/{Id}")]
        public async Task<IActionResult> Update(UpdateUserCommand updateUserCommand)
        {
            await Mediator.Send(updateUserCommand);

            return NoContent();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
        {
            var query = new GetListUserQuery
            {
                PageRequest = pageRequest
            };

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserQuery getByIdUserQuery)
        {
            var result = await Mediator.Send(getByIdUserQuery);

            return Ok(result);
        }

        [HttpPost("GetListByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest , [FromBody] Dynamic dynamic)
        {
            var query = new GetListByDynamicUserQuery()
            {
                PageRequest = pageRequest,
                Dynamic = dynamic
            };

            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
