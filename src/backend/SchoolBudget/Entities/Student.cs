namespace SchoolBudget.Entities;

public sealed class Student : BaseEntity<Student>
{
    public string Name { get; private set; } = null!;
    public string? Comment { get; set; }
}