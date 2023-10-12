using Microsoft.EntityFrameworkCore;
using SchoolBudget.Entities;

namespace SchoolBudget.Dal;

public class SchoolDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Adult> Adults { get; set; }
    public DbSet<AdultLink> AdultLinks { get; set; }

    public DbSet<Fund> Funds { get; set; }
    public DbSet<Fundraising> Fundraising { get; set; }
    public DbSet<FundraisingMember> FundraisingMembers { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseItem> ExpenseItems { get; set; }

    public async Task<T> InTransaction<T>(Func<Task<T>> func)
    {
        using var transaction = await Database.BeginTransactionAsync();
        var result = await func();
        await SaveChangesAsync();
        await transaction.CommitAsync();
        return result;
    }

    public async Task InTransaction(Func<Task> func)
    {
        using var transaction = await Database.BeginTransactionAsync();
        await func();
        await SaveChangesAsync();
        await transaction.CommitAsync();
    }
}