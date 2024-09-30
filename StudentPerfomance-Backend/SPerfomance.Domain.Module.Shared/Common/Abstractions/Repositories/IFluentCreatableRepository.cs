using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

public interface IFluentCreatableRepository<TEntity> where TEntity : Entity
{
	Task<TEntity> FluentCreate(TEntity entity);
}
