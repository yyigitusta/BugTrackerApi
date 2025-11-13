namespace BugTrackerApi.Dtos;
public record ProjectCreateDto(string Name,string? Description);
public record ProjectUpdateDto(string Name,string? Description);
public record ProjectReadDto(int Id,string Name,string? Description,DateTime CreatedAt);

