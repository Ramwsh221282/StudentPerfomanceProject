using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

public interface IForceUpdatableRepository<TEntity> where TEntity : Entity
{
	Task<TEntity> ForceUpdate(TEntity entity);
}
