using server.Models;
using System;
using System.Collections.Generic;

namespace server.Services
{
    public interface ITodoRepository
    {
        IEnumerable<TodoItem> GetAll(bool? done);
        TodoItem GetSingle(Guid id);
        void Add(TodoItem item);
        TodoItem Update(Guid id, TodoItem item);
        void Delete(Guid id);
    }
}
