using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolBudget.Dal;
using SchoolBudget.Entities;

namespace SchoolBudget.Controllers;

[Route("api/student")]
public class StudentsController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public StudentsController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<IReadOnlyCollection<Student>> GetStudents() =>
        await _context.Students.ToListAsync();
}
