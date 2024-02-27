namespace SchoolBudget.Entities;

public interface IIdentified<T>
{
    Id<T> Id { get; set; }
}