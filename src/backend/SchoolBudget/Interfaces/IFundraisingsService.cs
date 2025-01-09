using SchoolBudget.Dto;
using SchoolBudget.Entities;

namespace SchoolBudget.Interfaces;

public interface IFundraisingsService
{
    Task<Fundraising> CreateRegular(FundraisingEditDto dto);
    Task<Fundraising> CreatePeriodic(FundraisingEditDto dto);
    Task<Fundraising> CreateTargeted(FundraisingEditDto dto);

    Task<List<Fundraising>> GetByFund(Id<Fund> fundId);
    Task<List<Fundraising>> GetByFundType(FundType fundType);
}
