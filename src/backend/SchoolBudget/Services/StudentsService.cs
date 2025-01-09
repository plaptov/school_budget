using Microsoft.EntityFrameworkCore;
using SchoolBudget.Dal;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;
using SchoolBudget.Models;

namespace SchoolBudget.Services;

public interface IStudentService : IRepository<StudentDto> {}

public class StudentsService : IStudentService
{
    private readonly IRepository<Student> _repository;
    private readonly SchoolDbContext _dbContext;

    public StudentsService(IRepository<Student> repository, SchoolDbContext dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
    }

    public async ValueTask<StudentDto> Add(StudentDto dto)
    {
        var result = await _repository.Add(ToEntity(dto));
        return (await Get(result.Id))!;
    }

    public ValueTask Delete(Id<StudentDto> id) => _repository.Delete(Id<Student>.From(id));

    public ValueTask<StudentDto?> Get(Id<StudentDto> id) => Get(Id<Student>.From(id));

    public async ValueTask<StudentDto?> Get(Id<Student> id) =>
        ToDto(await GetQuery().Where(s => s.Id == id).SingleOrDefaultAsync());

    public IQueryable<StudentDto> Query() => GetQuery().Select(x => ToDto(x)!);

    public async ValueTask<StudentDto> Update(StudentDto dto)
    {
        var student = await GetQuery().Where(s => s.Id == dto.Id).SingleAsync();
        _dbContext.Entry(student).CurrentValues.SetValues(ToEntity(dto));
        foreach (var link in student.AdultLinks!.Where(l => !dto.Adults.Contains(l.AdultId)))
            _dbContext.Remove(link);
        foreach (var adultId in dto.Adults.Where(a =>!student.AdultLinks!.Any(l => l.AdultId == a)))
        {
            _dbContext.Add(new AdultLink
            {
                AdultId = adultId,
                StudentId = dto.Id,
            });
        }
        await _dbContext.SaveChangesAsync();
        return (await Get(dto.Id))!;
    }

    private IQueryable<Student> GetQuery() => _repository.Query(q => q.Include(s => s.AdultLinks));

    private static StudentDto? ToDto(Student? student) => student is null ? null : new()
    {
        Id = student.Id,
        Name = student.Name,
        Comment = student.Comment,
        Adults = student.AdultLinks?.Select(a => a.AdultId).ToArray() ?? Array.Empty<Id<Adult>>(),
    };

    private static Student ToEntity(StudentDto dto)
    {
        var student = new Student
        {
            Id = dto.Id,
            Name = dto.Name,
            Comment = dto.Comment,
        };
        student.AdultLinks = dto.Adults.Select(a => new AdultLink
        {
            AdultId = a,
            Student = student
        }).ToList();

        return student;
    }
}