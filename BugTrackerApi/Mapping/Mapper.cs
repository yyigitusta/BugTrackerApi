using BugTrackerApi.Dtos;
using BugTrackerApi.Models;

namespace BugTrackerApi.Mapping;

public static class Mapper
{
    public static ProjectReadDto ToReadDto(this Project p)
        => new(p.Id, p.Name, p.Description, p.CreatedAt);

    public static IssueReadDto ToReadDto(this Issue i)
        => new(i.Id, i.Title, i.Body, i.Status, i.ProjectId, i.CreatedAt);
}
