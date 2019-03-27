using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Hubs;
using server.Services;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IHubContext<TodoHub> _todoHubContext;
        private readonly ITodoRepository todoRepository;

        public TodosController(IHubContext<TodoHub> todoHubContext, ITodoRepository todoRepository)
        {
            _todoHubContext = todoHubContext;
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get(bool? done)
        {
            return Ok(todoRepository.GetAll(done));
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetSingle(Guid id)
        {
            return Ok(todoRepository.GetSingle(id));
        }

        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody] TodoItem todoItem)
        {
            todoItem.Created = DateTime.Now;
            todoRepository.Add(todoItem);

            _todoHubContext.Clients.All.SendAsync("itemAdded", todoItem);

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public ActionResult<TodoItem> Put(Guid id, [FromBody] TodoItem todoItem)
        {
            var newTodo = todoRepository.Update(id, todoItem);

            _todoHubContext.Clients.All.SendAsync("itemUpdated", todoItem);

            return Ok(newTodo);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> Delete(Guid id)
        {
            todoRepository.Delete(id);

            return NoContent();
        }
    }

    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool Done { get; set; }
        public DateTime Created { get; set; }
    }
}