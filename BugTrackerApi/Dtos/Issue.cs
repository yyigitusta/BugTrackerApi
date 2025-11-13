using BugTrackerApi.Models;
namespace BugTrackerApi.Dtos;
public record IssueCreateDto(string Title,string? Body,IssueStatus Status,int ProjectId);
public record IssueUpdateDto(string Title, string? Body, IssueStatus Status);
public record IssueReadDto(int Id,string Title, string? Body, IssueStatus Status,int ProjectId,DateTime CreatedAt);


