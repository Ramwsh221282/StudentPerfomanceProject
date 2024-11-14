using System.Linq.Expressions;

namespace SPerfomance.Domain.Abstractions;

public interface IRepositoryQuery<TEntity>
{
    public Expression<Func<TEntity, bool>> CreateSpecification();
}
