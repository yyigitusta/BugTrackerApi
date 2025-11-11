using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BugTrackerApi.Data;
using BugTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(AppDbContext db) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll() =>
            Ok(await db.Projects.AsNoTracking().OrderByDescending(p => p.CreatedAt).ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> GetById(int id)
        {
            var p = await db.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return p is null ? NotFound() : Ok(p);
        }
        [HttpPost]
        public async Task<ActionResult<Project>> Create([FromBody] Project input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                return BadRequest(new { message = "Name is required" });

            var entity = new Project { Name = input.Name, Description = input.Description };
            db.Projects.Add(entity);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);


        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Project input)
        {
            var p = await db.Projects.FindAsync(id);
            if (p is null) return NotFound();

            if (string.IsNullOrEmpty(input.Name))
            {
                return BadRequest(new { message = ("Name is required") });
            }
            p.Name = input.Name;
            p.Description = input.Description;
            await db.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p=await db.Projects.FindAsync(id);
            if (p is null) return NotFound();

            db.Projects.Remove(p);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
