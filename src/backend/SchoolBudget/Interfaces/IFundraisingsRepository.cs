using SchoolBudget.Entities;

namespace SchoolBudget.Interfaces;

public interface IFundraisingsRepository : IRepository<Fundraising>
{
    Task<Fundraising> GetLastFundraising(Id<Fund> fundId);
    Task<List<Id<Student>>> GetMembers(Id<Fundraising> id);
}
