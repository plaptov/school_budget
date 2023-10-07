namespace SchoolBudget.Entities;

public class Adult
{
    public long Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Phone { get; private set; }
    public string? Comment { get; set; }
}