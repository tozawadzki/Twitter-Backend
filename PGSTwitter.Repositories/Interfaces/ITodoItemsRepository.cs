namespace PGSTwitter.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITodoItemsRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task UpdateTodoItem(long id, TodoItem newTodoItem);
        Task<TodoItem> CreateTodoItem(TodoItem todoItem);
        Task DeleteTodoItem(long id);
        bool TodoItemExists(long id);
    }
}