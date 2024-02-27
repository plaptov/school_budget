using Microsoft.AspNetCore.Mvc;
using SchoolBudget.Models;
using SchoolBudget.Services;

namespace SchoolBudget.Controllers;

[Route("api/student")]
public class StudentsController : BaseApiController<StudentDto>
{
    public StudentsController(IStudentService studentService) : base(studentService)
    {
    }
}
