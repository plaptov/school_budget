namespace SchoolBudget.Entities;

public sealed class ExpenseItem : BaseEntity<ExpenseItem>
{
    public decimal? Cost { get; set; }
    public int? Quantity { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
}
