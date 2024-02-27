namespace SchoolBudget.Entities;

public sealed class Student : BaseEntity<Student>
{
    public string Name { get; set; } = null!;
    public string? Comment { get; set; }
    public List<AdultLink>? AdultLinks { get; set; }
}