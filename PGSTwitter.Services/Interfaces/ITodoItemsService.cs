namespace PGSTwitter.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO> GetTodoItem(long id);
        Task UpdateTodoItem(long id, TodoItemDTO todoItemDto);
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDto);
        Task DeleteTodoItem(long id);
        bool TodoItemExists(long id);
    }
}