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
    public class UserUseCaseController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UserUseCaseController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UserUseCaseController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserUseCaseSearch search, [FromServices] IGetUserUseCasesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<UserUseCaseController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSingleUserUseCaseQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<UserUseCaseController>
        [HttpPost]
        public IActionResult Post([FromBody] UserUseCaseDto dto, [FromServices] ICreateUserUseCaseCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<UserUseCaseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserUseCaseCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
