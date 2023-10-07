namespace SchoolBudget.Entities;

public class Payment
{
    public long Id { get; set; }

    public Student Student { get; set; } = null!;

    public Adult Adult { get; set; } = null!;

    public IReadOnlyCollection<Income> Incomes { get; set; } = null!;

    public DateOnly Date { get; set; }

    public decimal Sum { get; set; }
}
