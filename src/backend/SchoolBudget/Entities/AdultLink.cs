namespace SchoolBudget.Entities;

public sealed class AdultLink
{
    public Id<Adult> AdultId { get; set; }
    public Adult Adult { get; set; } = null!;
    public Id<Student> StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public bool IsDefault { get; set; }
    public string? Comment { get; set; }
}
