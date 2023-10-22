using Microsoft.AspNetCore.Mvc;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Controllers;

[Route("api/student")]
public class StudentsController : BaseApiController<Student>
{
    public StudentsController(IRepository<Student> repository) : base(repository)
    {
    }
}
