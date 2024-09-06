using FlyAway.Application;
using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyAway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public TagController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<TagController>
        [HttpGet]
        public IActionResult Get([FromQuery] TagSearch search, [FromServices] IGetTagsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSingleTagQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<TagController>
        [HttpPost]
        public IActionResult Post([FromBody] TagDto dto, [FromServices] ICreateTagCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TagDto dto, [FromServices] IUpdateTagCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteTagCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
