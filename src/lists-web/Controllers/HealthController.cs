using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace list.Controllers
{
    [AllowAnonymous]
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("live")]
        public IActionResult LivenessProbe()
        {
            return Ok();
        }

        [HttpGet]
        [Route("ready")]
        public IActionResult ReadinessProbe()
        {
            return Ok();
        }
    }
}