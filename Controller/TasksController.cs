using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;

namespace TaskApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TasksController : ControllerBase
  {
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet("getAllTasks")]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
    {
      var tasks = await _context.Tasks.ToListAsync();
      return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Task>> GetTask(int id)
    {
      var task = await _context.Tasks.FindAsync(id);
      if (task == null) return NotFound();
      
      return task;
    }


    [HttpPost("createTask")]
    public async Task<ActionResult<Models.Task>> CreateTask([FromBody] Models.Task task)
    {
      if (task == null) return BadRequest();

      task.RegisterDate = DateTime.Now;
      _context.Tasks.Add(task);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTask", new { id = task.Id }, task);
    }

    [HttpPut("updateTask/{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] Models.Task task)
    {
      if (id != task.Id) return BadRequest();

      var existingTask = await _context.Tasks.FindAsync(id);
      if (existingTask == null) return NotFound();

      existingTask.Title = task.Title;
      existingTask.Description = task.Description;
      existingTask.Status = task.Status;

      await _context.SaveChangesAsync();

      return Ok(existingTask);
    }

    [HttpDelete("deleteTask/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
      var task = await _context.Tasks.FindAsync(id);
      if (task == null) return NotFound();

      _context.Tasks.Remove(task);
      await _context.SaveChangesAsync();

      return NoContent();
    }

  }

}