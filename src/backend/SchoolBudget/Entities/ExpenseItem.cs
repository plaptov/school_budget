namespace SchoolBudget.Entities;

public class ExpenseItem
{
    public long Id { get; set; }
    public decimal? Cost { get; set; }
    public int? Quantity { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
}
