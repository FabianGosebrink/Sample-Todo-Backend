using server.Models;
using System;
using System.Collections.Generic;

namespace server.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<TodoEntity> GetAll(bool? done);
        TodoEntity GetSingle(Guid id);
        TodoEntity Add(TodoEntity item);
        TodoEntity Update(Guid id, TodoEntity item);
        void Delete(TodoEntity item);

        bool Save();
    }
}
