using Microsoft.EntityFrameworkCore;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Dal;

public class FundsRepository(SchoolDbContext context) : BaseRepository<Fund>(context), IFundsRepository
{
    public Task<Fund> GetDefaultFund() => Query().FirstAsync(f => f.Type == FundType.Default);
}
