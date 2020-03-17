namespace PGSTwitter.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repositories.Interfaces;

    public class TodoItemsService : ITodoItemsService
    {
        private readonly ITodoItemsRepository _itemsRepository;
        private readonly ITodoItemsMapper _mapper;

        public TodoItemsService(ITodoItemsRepository itemsRepository, ITodoItemsMapper mapper)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            var todoItems = await _itemsRepository.GetTodoItems();
            var todoItemsDtos = todoItems.Select(i => _mapper.ItemToDTO(i)).ToList();
            return todoItemsDtos;
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _itemsRepository.GetTodoItem(id);
            if (todoItem == null)
            {
                return null;
            }

            var todoItemsDto = _mapper.ItemToDTO(todoItem);
            return todoItemsDto;
        }

        public async Task UpdateTodoItem(long id, TodoItemDTO todoItemDto)
        {
            var todoItem = _mapper.DTOToItem(todoItemDto);
            try
            {
                await _itemsRepository.UpdateTodoItem(id, todoItem);
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                throw;
            }
        }

        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDto)
        {
            var todoItem = _mapper.DTOToItem(todoItemDto);
            var createdItem = await _itemsRepository.CreateTodoItem(todoItem);
            var createdItemDto = _mapper.ItemToDTO(createdItem);
            return createdItemDto;
        }

        public async Task DeleteTodoItem(long id)
        {
            await _itemsRepository.DeleteTodoItem(id);
        }

        public bool TodoItemExists(long id)
        {
            var exists = _itemsRepository.TodoItemExists(id);
            return exists;
        }
    }
}
