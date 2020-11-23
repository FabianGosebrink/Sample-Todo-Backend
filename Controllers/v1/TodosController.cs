using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Extensions;
using server.Hubs;
using server.Models;
using server.Repositories;

namespace server.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IHubContext<TodoHub> _todoHubContext;
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodosController(
            IHubContext<TodoHub> todoHubContext, 
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _todoHubContext = todoHubContext;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoDto>> Get([FromQuery] QueryParameters queryParameters)
        {
            var items = _todoRepository.GetAll(queryParameters);

            var allItemCount = _todoRepository.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(items.Select(x => _mapper.Map<TodoDto>(x)));
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetSingle))]
        public ActionResult<TodoDto> GetSingle(Guid id)
        {
            TodoEntity entity = _todoRepository.GetSingle(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TodoDto>(entity));
        }

        [HttpPost(Name = nameof(AddTodo))]
        public ActionResult AddTodo(ApiVersion version, [FromBody] TodoCreateDto todoCreateDto)
        {
            if (todoCreateDto == null)
            {
                return BadRequest();
            }

            TodoEntity item = _mapper.Map<TodoEntity>(todoCreateDto);
            item.Created = DateTime.UtcNow;
            TodoEntity newTodoEntity = _todoRepository.Add(item);

            if (!_todoRepository.Save())
            {
                throw new Exception("Adding an item failed on save.");
            }

            _todoHubContext.Clients.All.SendAsync("todo-added", newTodoEntity);

            return CreatedAtRoute(
                nameof(GetSingle),
                new { version = version.ToString(), id = newTodoEntity.Id },
                _mapper.Map<TodoDto>(newTodoEntity));
        }

        [HttpPut]
        [Route("{id}", Name = nameof(UpdateTodo))]
        public ActionResult<TodoDto> UpdateTodo(Guid id, [FromBody] TodoUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }

            TodoEntity singleById = _todoRepository.GetSingle(id);

            if (singleById == null)
            {
                return NotFound();
            }

            updateDto.Value = updateDto.Value ?? singleById.Value;
            _mapper.Map(updateDto, singleById);

            TodoEntity updatedTodo = _todoRepository.Update(id, singleById);

            if (!_todoRepository.Save())
            {
                throw new Exception("Updating an item failed on save.");
            }

            var updatedDto = _mapper.Map<TodoDto>(updatedTodo);

            _todoHubContext.Clients.All.SendAsync("todo-updated", updatedDto);

            return Ok(updatedDto);
        }

        [HttpDelete]
        [Route("{id}", Name = nameof(DeleteTodo))]
        public ActionResult DeleteTodo(Guid id)
        {
            TodoEntity singleById = _todoRepository.GetSingle(id);

            if (singleById == null)
            {
                return NotFound();
            }

            _todoRepository.Delete(singleById);

            if (!_todoRepository.Save())
            {
                throw new Exception("Deleting an item failed on save.");
            }

            _todoHubContext.Clients.All.SendAsync("todo-deleted");

            return NoContent();
        }
    }
}