namespace SchoolBudget.Entities;

public class Adult : BaseEntity<Adult>
{
    public string Name { get; private set; } = null!;
    public string? Phone { get; private set; }
    public string? Comment { get; set; }
}