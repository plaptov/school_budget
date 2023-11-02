namespace SchoolBudget.Entities;

public sealed class Adult : BaseEntity<Adult>
{
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Comment { get; set; }
}