using Microsoft.AspNetCore.Mvc;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Controllers
{
    [Route("api/fund")]
    [ApiController]
    public class FundsController(IRepository<Fund> repository) : BaseApiController<Fund>(repository)
    {
    }
}
