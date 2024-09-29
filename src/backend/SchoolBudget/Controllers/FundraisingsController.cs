using Microsoft.AspNetCore.Mvc;
using SchoolBudget.Dto;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Controllers
{
    [Route("api/fundraisings")]
    [ApiController]
    public class FundraisingsController(IRepository<Fundraising> repository, IFundraisingsService _service)
        : BaseApiController<Fundraising>(repository)
    {
        [HttpGet("byFund/{fundId}")]
        public Task<List<Fundraising>> GetByFund(Id<Fund> fundId) => _service.GetByFund(fundId);

        [HttpGet("byFundType/{fundType}")]
        public Task<List<Fundraising>> GetByFundType(FundType fundType) => _service.GetByFundType(fundType);

        [HttpPost("regular")]
        public Task<Fundraising> CreateRegular([FromBody] FundraisingEditDto dto) => _service.CreateRegular(dto);

        [HttpPost("periodic")]
        public Task<Fundraising> CreatePeriodic([FromBody] FundraisingEditDto dto) => _service.CreatePeriodic(dto);

        [HttpPost("targeted")]
        public Task<Fundraising> CreateTargeted([FromBody] FundraisingEditDto dto) => _service.CreateTargeted(dto);
    }
}
