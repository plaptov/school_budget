using Microsoft.EntityFrameworkCore;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Dal;

public class FundraisingsRepository(SchoolDbContext context)
 : BaseRepository<Fundraising>(context), IFundraisingsRepository
{
    public Task<Fundraising> GetLastFundraising(Id<Fund> fundId) => Query()
        .Where(f => f.FundId == fundId)
        .OrderByDescending(f => f.Date)
        .FirstAsync();

    public Task<List<Id<Student>>> GetMembers(Id<Fundraising> id) => Query()
        .Where(f => f.Id == id)
        .SelectMany(f => f.Members)
        .Select(f => f.StudentId)
        .ToListAsync();
}