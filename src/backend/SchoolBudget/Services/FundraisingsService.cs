using Microsoft.EntityFrameworkCore;
using SchoolBudget.Dal;
using SchoolBudget.Dto;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Services;

public class FundraisingsService(
    IRepository<Fundraising> _fundraisingRepository,
    IFundsRepository _fundRepository,
    SchoolDbContext _dbContext)
    : IFundraisingsService
{
    public async Task<Fundraising> CreateRegular(FundraisingEditDto dto)
    {
        var fund = await _fundRepository.GetDefaultFund();
        return await InternalCreate(fund, FundraisingType.OneTime, dto);
    }

    public async Task<Fundraising> CreatePeriodic(FundraisingEditDto dto)
    {
        var fund = new Fund()
        {
            Type = FundType.Continuous,
            Name = dto.Name,
        };
        await _fundRepository.Add(fund);
        return await InternalCreate(fund, FundraisingType.Periodic, dto);
    }

    public async Task<Fundraising> CreateTargeted(FundraisingEditDto dto)
    {
        var fund = new Fund()
        {
            Type = FundType.Targeted,
            Name = dto.Name,
        };
        await _fundRepository.Add(fund);
        return await InternalCreate(fund, FundraisingType.OneTime, dto);
    }

    private async Task<Fundraising> InternalCreate(Fund fund, FundraisingType type, FundraisingEditDto dto)
    {
        var fundraising = new Fundraising
        {
            Fund = fund,
            Type = type,
        };
        fundraising.Assign(dto);
        await _fundraisingRepository.Add(fundraising);
        return fundraising;
    }

    public Task<List<Fundraising>> GetByFund(Id<Fund> fundId) => _dbContext
        .Fundraising.AsNoTracking()
        .Where(fr => fr.FundId == fundId)
        .ToListAsync();

    public Task<List<Fundraising>> GetByFundType(FundType fundType) => _dbContext
        .Fundraising.AsNoTracking()
        .Where(fr => fr.Fund.Type == fundType)
        .ToListAsync();
}
