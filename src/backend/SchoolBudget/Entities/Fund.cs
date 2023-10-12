namespace SchoolBudget.Entities;

public enum FundType
{
    Default = 0,
    Targeted = 1,
    Continuous = 2,
}

public class Fund : BaseEntity<Fund>
{
    public string Name { get; set; } = null!;
    public FundType Type { get; init; }
    public bool IsClosed { get; private set; }

    public bool CanClose => Type == FundType.Targeted;

    public void Close()
    {
        if (!CanClose)
            throw new InvalidOperationException();

        IsClosed = true;
    }
}
