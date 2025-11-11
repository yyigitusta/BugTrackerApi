using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { message = "pong" });
    }
}
