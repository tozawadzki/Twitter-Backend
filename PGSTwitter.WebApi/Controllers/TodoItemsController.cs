namespace PGSTwitter.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Repositories.Models;
    using Services.Implementations;
    using Services.Interfaces;
    using Services.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _itemsService;

        public TodoItemsController(ITodoItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        /// Gets all todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var todoItemDtos = await _itemsService.GetTodoItems();
            return Ok(todoItemDtos);
        }

        /// <summary>
        /// Gets todo item with specific id
        /// </summary>
        /// <param name="id">Id of requested todo item</param>
        /// <returns>todo item with requested id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            var todoItemDto = await _itemsService.GetTodoItem(id);
            if (todoItemDto == null)
            {
                return NotFound();
            }

            return Ok(todoItemDto);
        }

        /// <summary>
        /// Updates todo item with specific id
        /// </summary>
        /// <param name="id">Id of todo item to be updated</param>
        /// <param name="todoItemDto">DTO of new todo item's body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            var exists = _itemsService.TodoItemExists(id);
            if (!exists)
            {
                return NotFound();
            }

            try
            {
                await _itemsService.UpdateTodoItem(id, todoItemDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates new todo item
        /// </summary>
        /// <param name="todoItemDto">Todo item to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDto)
        {
            var createdTodoItemDto = await _itemsService.CreateTodoItem(todoItemDto);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = createdTodoItemDto.Id },
                createdTodoItemDto);
        }

        /// <summary>
        /// Deletes todo item with specific id
        /// </summary>
        /// <param name="id">Id of todo item to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var exists = _itemsService.TodoItemExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _itemsService.DeleteTodoItem(id);
            return NoContent();
        }
    }
}