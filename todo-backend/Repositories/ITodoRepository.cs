﻿using TodoBackend.Models;
using System;
using System.Linq;

namespace TodoBackend.Repositories
{
    public interface ITodoRepository
    {
        IQueryable<TodoEntity> GetAll(QueryParameters queryParameters);
        TodoEntity GetSingle(Guid id);
        TodoEntity Add(TodoEntity item);
        TodoEntity Update(Guid id, TodoEntity item);
        void Delete(TodoEntity item);
        int Count();
        bool Save();
    }
}
