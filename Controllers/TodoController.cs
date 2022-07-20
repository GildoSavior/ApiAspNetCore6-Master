using Microsoft.AspNetCore.Mvc;
using ApiBalta.Data;
using Microsoft.EntityFrameworkCore;
using ApiBalta.ViweModels;
using ApiBalta.Models;

namespace ApiBalta.Controllers
{
    [Route("v1")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDBContext context)
        {
            var todos = await context.todos.AsNoTracking().ToListAsync();

            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIAsync([FromServices] AppDBContext context, int id)
        {
            var todo = await context.todos.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);

            return todo != null ? Ok(todo) : NotFound("Todo nao existente!!");
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDBContext context,
            [FromBody] CreateTodoVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title,
            };

            try
            {
                await context.todos.AddAsync(todo);
                await context.SaveChangesAsync();

                return Created($"v1/todos/{todo.ID}", todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDBContext context,
            [FromBody] CreateTodoVM model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = await context.todos.FirstOrDefaultAsync(x => x.ID == id);

            if (todo == null) NotFound("Todo inexistente!!");

            try
            {
                todo.Title = model.Title;

                context.todos.Update(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDBContext context, [FromRoute] int id)
        {
            var todo = await context.todos.FirstOrDefaultAsync(x => x.ID == id);

            try
            {
                context.todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}