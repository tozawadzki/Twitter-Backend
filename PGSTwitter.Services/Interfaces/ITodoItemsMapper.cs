namespace PGSTwitter.Services.Interfaces
{
    using Models;
    using Repositories.Models;

    public interface ITodoItemsMapper
    {
        TodoItemDTO ItemToDTO(TodoItem todoItem);
        TodoItem DTOToItem(TodoItemDTO todoItemDTO);
    }
}