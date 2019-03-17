namespace Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Process.Features.Course;

    [Route("api/sync/courses")]
    [ApiController]
    public class SynchronousController : ControllerBase
    {
        readonly IMediator mediator;

        public SynchronousController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost, Route("{id}")]
        [ProducesResponseType(typeof(HttpStatusCode), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(HttpStatusCode), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> Create(
            [FromBody] Create.Command command)
        {
            await mediator.Send(command);
            return Created($"api/sync/courses/{command.Id}", null);
        }

        [HttpPost, Route("{id}/students")]
        [ProducesResponseType(typeof(HttpStatusCode), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(HttpStatusCode), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(HttpStatusCode), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> SignUp(
            [FromBody] SignUp.Command command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
