namespace SchoolBudget.Entities;

public class Student : BaseEntity<Student>
{
    public string Name { get; private set; } = null!;
    public string? Comment { get; set; }
}