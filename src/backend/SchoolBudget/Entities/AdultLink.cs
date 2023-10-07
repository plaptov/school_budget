namespace SchoolBudget.Entities;
public class AdultLink
{
    public long AdultId { get; set; }
    public Adult Adult { get; set; } = null!;
    public long StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public bool IsDefault { get; set; }
    public string? Comment { get; set; }
}
