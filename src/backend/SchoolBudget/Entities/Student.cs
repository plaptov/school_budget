namespace SchoolBudget.Entities;

public class Student
{
    public long Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Comment { get; set; }
}