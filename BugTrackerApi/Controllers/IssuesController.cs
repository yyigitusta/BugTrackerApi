using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTrackerApi.Data;
using BugTrackerApi.Models;
using BugTrackerApi.Dtos;
using BugTrackerApi.Mapping;

namespace BugTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IssueReadDto>>> GetAll(
        [FromQuery] int? projectId,
        [FromQuery] IssueStatus? status)
    {
        var q = db.Issues.AsNoTracking().AsQueryable();
        if (projectId is not null) q = q.Where(i => i.ProjectId == projectId);
        if (status is not null) q = q.Where(i => i.Status == status);

        var list = await q.OrderByDescending(i => i.CreatedAt).ToListAsync();
        return Ok(list.Select(i => i.ToReadDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IssueReadDto>> GetById(int id)
    {
        var issue = await db.Issues.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        return issue is null ? NotFound() : Ok(issue.ToReadDto());
    }

    [HttpPost]
    public async Task<ActionResult<IssueReadDto>> Create([FromBody] IssueCreateDto dto)
    {
        var projectExists = await db.Projects.AnyAsync(p => p.Id == dto.ProjectId);
        if (!projectExists) return BadRequest(new { message = "Project not found." });

        var entity = new Issue
        {
            Title = dto.Title,
            Body = dto.Body,
            Status = dto.Status,
            ProjectId = dto.ProjectId
        };

        db.Issues.Add(entity);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToReadDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] IssueUpdateDto dto)
    {
        var issue = await db.Issues.FindAsync(id);
        if (issue is null) return NotFound();

        issue.Title = dto.Title;
        issue.Body = dto.Body;
        issue.Status = dto.Status;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var issue = await db.Issues.FindAsync(id);
        if (issue is null) return NotFound();

        db.Issues.Remove(issue);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
