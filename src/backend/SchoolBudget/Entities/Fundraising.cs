namespace SchoolBudget.Entities;

public enum FundraisingType
{
    OneTime = 0,
    Periodic = 1,
}

public class Fundraising : BaseEntity<Fundraising>
{
    public DateOnly Date { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal RecommendedAmount { get; set; }
    public bool IsClosed { get; set; }
    public DateOnly? ClosingDate { get; set; }

    public List<FundraisingMember> Members { get; set; } = null!;
}