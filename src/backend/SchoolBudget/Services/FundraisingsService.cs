using Microsoft.EntityFrameworkCore;
using SchoolBudget.Dal;
using SchoolBudget.Dto;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Services;

public class FundraisingsService(
    IFundraisingsRepository _fundraisingRepository,
    IFundsRepository _fundRepository,
    SchoolDbContext _dbContext)
    : IFundraisingsService
{
    public async Task<Fundraising> CreateRegular(FundraisingEditDto dto)
    {
        var fund = await _fundRepository.GetDefaultFund();
        return await InternalCreate(fund, dto);
    }

    public async Task<Fundraising> CreateTargeted(FundraisingEditDto dto)
    {
        var fund = new Fund()
        {
            Type = FundType.Targeted,
            Name = dto.Name,
        };
        await _fundRepository.Add(fund);
        return await InternalCreate(fund, dto);
    }

    public async Task<Fundraising> CreatePeriodical(FundraisingEditDto dto)
    {
        var fund = new Fund()
        {
            Type = FundType.Periodical,
            Name = dto.Name,
        };
        await _fundRepository.Add(fund);
        return await InternalCreate(fund, dto);
    }

    public async Task<Fundraising> CopyLastPeriodicalFundraising(Id<Fund> fundId)
    {
        var fund = await _fundRepository.Get(fundId);
        if (fund is null || fund.Type != FundType.Periodical)
            throw new InvalidOperationException();

        var last = await _fundraisingRepository.GetLastFundraising(fundId);
        var students = await _fundraisingRepository.GetMembers(last.Id);
        var newFundraising = new Fundraising()
        {
            FundId = fundId,
            Name = last.Name,
            Date = last.Date.AddMonths(1),
            Description = last.Description,
            RecommendedAmount = last.RecommendedAmount,
        };
        newFundraising.Members = [.. students.Select(s => new FundraisingMember() { Fundraising = newFundraising, StudentId = s })];
        await _fundraisingRepository.Add(newFundraising);

        return (await _fundraisingRepository.Get(newFundraising.Id))!;
    }

    private async Task<Fundraising> InternalCreate(Fund fund, FundraisingEditDto dto)
    {
        var fundraising = new Fundraising
        {
            Fund = fund,
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
