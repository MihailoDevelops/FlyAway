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
    public class CommentLikeController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CommentLikeController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<CommentLikeController>
        [HttpGet]
        public IActionResult Get([FromQuery] CommentLikeSearch search, [FromServices] IGetCommentLikesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<CommentLikeController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentLikeDto dto, [FromServices] ICreateCommentLikeCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<CommentLikeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentLikeCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
