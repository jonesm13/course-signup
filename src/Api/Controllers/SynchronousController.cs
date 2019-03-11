namespace Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/sync/course")]
    [ApiController]
    public class SynchronousController : ControllerBase
    {
        [HttpPost("{id}/students")]
        public async Task<ActionResult> SignUp(
            [FromQuery] int id,
            [FromBody] SignUpModel model)
        {
            return NoContent();
        }
    }
}