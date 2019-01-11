using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using list.Managers;
using list.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace list.Controllers
{
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