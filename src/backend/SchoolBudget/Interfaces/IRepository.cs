using SchoolBudget.Entities;

namespace SchoolBudget.Interfaces;

public interface IRepository<T> where T : IIdentified<T>
{
    ValueTask<T?> Get(Id<T> id);
    ValueTask<T> Add(T entity);
    ValueTask<T> Update(T entity);
    ValueTask Delete(Id<T> id);
    IQueryable<T> Query();
    IQueryable<T> Query(Func<IQueryable<T>, IQueryable<T>> config) => config(Query());
}