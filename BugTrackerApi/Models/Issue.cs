namespace BugTrackerApi.Models
{
    public enum IssueStatus { Open=0,InProgress=1,Resolved=2,Closed=3}
    public class Issue
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Body { get; set; }
        public IssueStatus Status { get; set; } = IssueStatus.Open;

        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    }
}
