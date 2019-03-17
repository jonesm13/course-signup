namespace Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Process.Features.Course;

    [Route("api/asynchronous/courses")]
    [ApiController]
    public class AsynchronousController : ControllerBase
    {
        readonly IMediator mediator;

        public AsynchronousController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost, Route("{id}")]
        [ProducesResponseType(typeof(HttpStatusCode), (int) HttpStatusCode.Accepted)]
        public async Task<ActionResult> CreateAsync(
            [FromBody] RequestSignUp.Command request)
        {
            await mediator.Send(request);
            return Accepted();
        }
    }
}