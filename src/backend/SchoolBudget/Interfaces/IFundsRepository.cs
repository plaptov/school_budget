using SchoolBudget.Entities;

namespace SchoolBudget.Interfaces;

public interface IFundsRepository : IRepository<Fund>
{
    Task<Fund> GetDefaultFund();
}
