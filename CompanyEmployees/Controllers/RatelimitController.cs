using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("api/ratelimit")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class RatelimitController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetResult()
        {
            return Ok("You cannot send reqwuest more than 3 times in 5 minutes. Try this feature !!");
        }
    }
}
