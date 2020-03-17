namespace PGSTwitter.Repositories.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly TodoContext _context;

        public TodoItemsRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            var todoItems = await _context.TodoItems
                .ToListAsync();

            return todoItems;
        }

        public async Task<TodoItem> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return todoItem;
        }

        public async Task UpdateTodoItem(long id, TodoItem newTodoItem)
        {
            var oldTodoItem = await _context.TodoItems.FindAsync(id);
            if (oldTodoItem == null)
            {
                return;
            }

            oldTodoItem.Name = newTodoItem.Name;
            oldTodoItem.IsComplete = newTodoItem.IsComplete;
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public bool TodoItemExists(long id)
        {
            var exists = _context.TodoItems.Any(i => i.Id == id);
            return exists;
        }
    }
}
