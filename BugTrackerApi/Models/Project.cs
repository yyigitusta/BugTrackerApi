namespace BugTrackerApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Issue> Issues { get; set; } = new();
    }
}
