using Microsoft.EntityFrameworkCore;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Dal;

public class BaseRepository<T>(SchoolDbContext context) : IRepository<T> where T : BaseEntity<T>
{
    protected readonly SchoolDbContext _context = context;

    protected DbSet<T> Set => _context.Set<T>();

    public ValueTask<T?> Get(Id<T> id) => Set.FindAsync(id);

    public IQueryable<T> Query() => Set.AsQueryable();

    public async ValueTask<T> Add(T entity)
    {
        Set.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async ValueTask<T> Update(T entity)
    {
        var original = await Get(entity.Id)
            ?? throw new InvalidOperationException($"{typeof(T).Name} with id {entity.Id} not found");

        Set.Entry(original).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return original;
    }

    public async ValueTask Delete(Id<T> id)
    {
        var original = await Get(id)
            ?? throw new InvalidOperationException($"{typeof(T).Name} with id {id} not found");

        Set.Remove(original);
        await _context.SaveChangesAsync();
    }
}
