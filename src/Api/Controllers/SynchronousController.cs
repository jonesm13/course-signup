namespace Api.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Process.Features.Course;

    [Route("api/sync/course")]
    [ApiController]
    public class SynchronousController : ControllerBase
    {
        readonly IMediator mediator;

        public SynchronousController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{id}/students")]
        public async Task<ActionResult> SignUp(
            [FromBody] SignUp.Command command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
