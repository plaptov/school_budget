namespace SchoolBudget.Entities;

public class Income
{
    public long Id { get; set; }

    public Fund Fund { get; set; } = null!;

    public Payment? Payment { get; set; }

    public Fundraising? Fundraising { get; set; }

    public DateOnly Date { get; set; }
    public decimal Sum { get; set; }
    public string? Description { get; set; }
}
