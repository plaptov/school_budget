namespace SchoolBudget.Entities;

public class BaseEntity<T> where T : BaseEntity<T>
{
    public Id<T> Id { get; set; }
}
