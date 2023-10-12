namespace SchoolBudget.Entities;

public class Expense : BaseEntity<Expense>
{
    public Fund Fund { get; set; } = null!;
    public DateOnly Date { get; set; }
    public decimal Sum { get; set; }
    public string? Description { get; set; }

    public List<ExpenseItem> Items { get; set; } = new(0);
}
