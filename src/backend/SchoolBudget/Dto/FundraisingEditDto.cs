namespace SchoolBudget.Dto;

public class FundraisingEditDto
{
    public DateOnly Date { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal RecommendedAmount { get; set; }
}
