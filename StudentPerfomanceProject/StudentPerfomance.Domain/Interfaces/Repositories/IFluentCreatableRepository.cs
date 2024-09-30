using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Domain.Interfaces.Repositories;

public interface IFluentCreatableRepository<TEntity> where TEntity : Entity
{
	Task<TEntity> FluentCreate(TEntity entity);
}
