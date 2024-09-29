using SchoolBudget.Dto;

namespace SchoolBudget.Entities;

public enum FundraisingType
{
    OneTime = 0,
    Periodic = 1,
}

public sealed class Fundraising : BaseEntity<Fundraising>
{
    public Id<Fund> FundId { get; set; }
    public Fund Fund { get; set; } = null!;
    public FundraisingType Type { get; set; } = FundraisingType.OneTime;
    public DateOnly Date { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal RecommendedAmount { get; set; }
    public bool IsClosed { get; set; }
    public DateOnly? ClosingDate { get; set; }

    public List<FundraisingMember> Members { get; set; } = null!;

    public void Assign(FundraisingEditDto dto)
    {
        Date = dto.Date;
        Name = dto.Name;
        Description = dto.Description;
        RecommendedAmount = dto.RecommendedAmount;
    }
}