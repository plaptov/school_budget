using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolBudget.Entities;
using SchoolBudget.Interfaces;

namespace SchoolBudget.Controllers;

public class BaseApiController<T> : ControllerBase where T : BaseEntity<T>
{
    private readonly IRepository<T> _repository;

    public BaseApiController(IRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet("all")]
    public Task<List<T>> GetAllEntities() => _repository.Query().AsNoTracking().ToListAsync();

    [HttpGet("{id}")]
    public ValueTask<T?> GetEntity([FromRoute] Id<T> id) => _repository.Get(id);

    [HttpPost]
    public ValueTask<T> AddEntity([FromBody] T entity) => _repository.Add(entity);

    [HttpPut]
    public ValueTask<T> UpdateEntity([FromBody] T entity) => _repository.Update(entity);

    [HttpDelete("{id}")]
    public ValueTask DeleteEntity([FromRoute] Id<T> id) => _repository.Delete(id);
}
