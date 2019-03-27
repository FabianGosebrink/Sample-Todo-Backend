using server.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public class TodoRepository : ITodoRepository
    {
        private ConcurrentDictionary<Guid, TodoItem> _store = new ConcurrentDictionary<Guid, TodoItem>();

        public IEnumerable<TodoItem> GetAll(bool? done)
        {
            if(done.HasValue)
            {
                return _store.Values.Where(x => x.Done == done.Value);
            }
            return _store.Values;
        }

        public TodoItem GetSingle(Guid id)
        {
            TodoItem item;
            return _store.TryGetValue(id, out item) ? item : null;
        }

        public void Add(TodoItem item)
        {
            Guid key = Guid.NewGuid();
            item.Id = key;

            if (!_store.TryAdd(item.Id, item))
            {
                throw new Exception("Item could not be added");
            }
        }

        public TodoItem Update(Guid id, TodoItem item)
        {
            _store.TryUpdate(id, item, GetSingle(id));
            return item;
        }

        public void Delete(Guid id)
        {
            TodoItem item;
            if (!_store.TryRemove(id, out item))
            {
                throw new Exception("Item could not be removed");
            }
        }
    }
}
