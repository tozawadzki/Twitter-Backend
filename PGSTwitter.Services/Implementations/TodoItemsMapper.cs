namespace PGSTwitter.Services.Implementations
{
    using Interfaces;
    using Models;
    using Repositories.Models;

    public class TodoItemsMapper : ITodoItemsMapper
    {
        public TodoItemDTO ItemToDTO(TodoItem todoItem)
        {
            var todoItemDto = new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

            return todoItemDto;
        }

        public TodoItem DTOToItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };

            return todoItem;
        }
    }
}
