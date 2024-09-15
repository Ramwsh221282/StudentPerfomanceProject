using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
	Task Commit();
	Task<int> Count();
	Task Create(TEntity entity);
	Task Remove(TEntity entity);
	Task<IReadOnlyCollection<TEntity>> GetAll();
	Task<IReadOnlyCollection<TEntity>> GetPaged(int page, int pageSize);
	Task<bool> HasEqualRecord(IRepositoryExpression<TEntity> expression);
	Task<int> CountWithExpression(IRepositoryExpression<TEntity> expression);
	Task<TEntity?> GetByParameter(IRepositoryExpression<TEntity> expression);
	Task<IReadOnlyCollection<TEntity>> GetFiltered(IRepositoryExpression<TEntity> expression);
	Task<IReadOnlyCollection<TEntity>> GetFilteredAndPaged(IRepositoryExpression<TEntity> expression, int page, int pageSize);
}
