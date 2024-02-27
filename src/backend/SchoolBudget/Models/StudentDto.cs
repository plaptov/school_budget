using SchoolBudget.Entities;

namespace SchoolBudget.Models;

public class StudentDto : IIdentified<Student>, IIdentified<StudentDto>
{
    public Id<Student> Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Comment { get; set; }
    public Id<Adult>[] Adults { get; set; } = Array.Empty<Id<Adult>>();
    Id<StudentDto> IIdentified<StudentDto>.Id
    {
        get => Id.To<StudentDto>();
        set => Id = Id<Student>.From(value);
    }
}