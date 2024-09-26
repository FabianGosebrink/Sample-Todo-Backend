using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TodoBackend.Models;

namespace TodoBackend.Controllers.v2
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