using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Queries.GetByIdOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Application.Features.Users.Queries.GetListByDynamicUser;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]CreateOperationClaimCommand createOperationClaimCommand)
        {
            var result = await Mediator.Send(createOperationClaimCommand);

            return Created("",result);
        }

        [HttpPut]
        [Route("Update/{Id}")]
        public async Task<IActionResult> Update(UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            await Mediator.Send(updateOperationClaimCommand);

            return NoContent();
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            await Mediator.Send(deleteOperationClaimCommand);

            return NoContent();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var query = new GetListOperationClaimQuery
            {
                PageRequest = pageRequest
            };

            await Mediator.Send(query);

            return Ok(query);
        }

        [HttpPost("GetListByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            var query = new GetListByDynamicUserQuery
            {
                PageRequest = pageRequest,
                Dynamic = dynamic
            };

            await Mediator.Send(query);

            return Ok(query);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdOperationClaimQuery getByIdOperationClaimQuery)
        {
            var result = Mediator.Send(getByIdOperationClaimQuery);

            return Ok(result);
        }
    }
}
