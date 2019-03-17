namespace Api.Controllers
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Process.Features.Health;

    [Route("health")]
    public class HealthController : ControllerBase
    {
        readonly IMediator mediator;

        public HealthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult> Get(Get.Query query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
