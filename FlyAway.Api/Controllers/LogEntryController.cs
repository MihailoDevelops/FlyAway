using FlyAway.Application;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using FlyAway.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyAway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogEntryController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public LogEntryController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<LogEntryController>
        [HttpGet]
        public IActionResult Get([FromQuery] LogEntrySearch search, [FromServices] IGetLogEntriesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<LogEntryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
