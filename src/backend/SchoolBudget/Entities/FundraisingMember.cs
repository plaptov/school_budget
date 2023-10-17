namespace SchoolBudget.Entities;

public sealed class FundraisingMember
{
    public Id<Fundraising> FundraisingId { get; set; }
    public Fundraising Fundraising { get; set; } = null!;

    public Id<Student> StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
