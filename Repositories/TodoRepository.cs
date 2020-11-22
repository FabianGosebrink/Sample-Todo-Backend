using server.Extensions;
using server.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace server.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoRepository(TodoDbContext dbContext)
        {
            _todoDbContext = dbContext;
        }

        public TodoEntity GetSingle(Guid id)
        {
            return _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);
        }

        public TodoEntity Add(TodoEntity item)
        {
            _todoDbContext.TodoItems.Add(item);
            return item;
        }

        public void Delete(TodoEntity item)
        {
            _todoDbContext.TodoItems.Remove(item);
        }

        public TodoEntity Update(Guid id, TodoEntity item)
        {
            _todoDbContext.TodoItems.Update(item);
            return item;
        }

        public IQueryable<TodoEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<TodoEntity> _allItems = _todoDbContext.TodoItems.OrderBy(queryParameters.OrderBy, queryParameters.IsDescending());

            if (queryParameters.Done.HasValue)
            {
                _allItems = _todoDbContext.TodoItems.Where(x => x.Done == queryParameters.Done.Value);
            }

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems.Where(x => x.Value.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _todoDbContext.TodoItems.Count();
        }

        public bool Save()
        {
            return (_todoDbContext.SaveChanges() >= 0);
        }
    }
}
