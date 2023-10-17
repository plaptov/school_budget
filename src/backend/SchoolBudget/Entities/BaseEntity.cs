namespace SchoolBudget.Entities;

public abstract class BaseEntity<T> where T : BaseEntity<T>
{
    public Id<T> Id { get; set; }
}
