using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetByIdTechnology;
using Application.Features.Technologies.Queries.GetList;
using Application.Features.Technologies.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto result = await Mediator.Send(createTechnologyCommand);

            return Created("",result);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            await Mediator.Send(deleteTechnologyCommand);

            return NoContent();
        }

        [HttpPut]
        [Route("Update/{Id}")]
        public async Task<IActionResult> Update(UpdateTechnologyCommand updateTechnologyCommand)
        {
            await Mediator.Send(updateTechnologyCommand);

            return NoContent();
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery query = new() { PageRequest = pageRequest };

            TechnologyGetListModel result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("GetListByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListByDynamicTechnologyQuery query = new() { PageRequest = pageRequest, Dynamic = dynamic };

            TechnologyGetListModel result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetByIdWithProgrammingLanguageName/{Id}")]
        public async Task<IActionResult> GetByIdWithProgrammingLanguageName([FromRoute] GetByIdWithProgrammingLanguageNameTechnologyQuery getByIdWithProgrammingLanguageNameTechnologyQuery)
        {
            TechnologyGetByIdWithProgrammingLanguageNameDto result =  await Mediator.Send(getByIdWithProgrammingLanguageNameTechnologyQuery);

            return Ok(result);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetList([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery)
        {
            TechnologyGetByIdDto result = await Mediator.Send(getByIdTechnologyQuery);

            return Ok(result);
        }
    }
}
