using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTrackerApi.Data;
using BugTrackerApi.Models;
using BugTrackerApi.Dtos;
using BugTrackerApi.Mapping;

namespace BugTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetAll()
        => Ok((await db.Projects.AsNoTracking()
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync())
            .Select(p => p.ToReadDto()));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectReadDto>> GetById(int id)
    {
        var p = await db.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return p is null ? NotFound() : Ok(p.ToReadDto());
    }

    [HttpPost]
    public async Task<ActionResult<ProjectReadDto>> Create([FromBody] ProjectCreateDto dto)
    {
        var entity = new Project { Name = dto.Name, Description = dto.Description };
        db.Projects.Add(entity);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToReadDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProjectUpdateDto dto)
    {
        var p = await db.Projects.FindAsync(id);
        if (p is null) return NotFound();
        p.Name = dto.Name;
        p.Description = dto.Description;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await db.Projects.FindAsync(id);
        if (p is null) return NotFound();
        db.Projects.Remove(p);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
