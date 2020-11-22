using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<TodoDto>> Get()
        {
            return Ok("2.0");
        }
    }
}