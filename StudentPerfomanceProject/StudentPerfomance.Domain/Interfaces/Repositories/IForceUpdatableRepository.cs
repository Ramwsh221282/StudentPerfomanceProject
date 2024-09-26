using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Domain.Interfaces.Repositories;

public interface IForceUpdatableRepository<TEntity> where TEntity : Entity
{
	Task<TEntity> ForceUpdate(TEntity entity);
}
