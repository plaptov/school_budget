using Microsoft.AspNetCore.Mvc;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Controllers;

[Route("api/adult")]
public class AdultsController : BaseApiController<Adult>
{
    public AdultsController(IRepository<Adult> repository) : base(repository)
    {
    }
}
