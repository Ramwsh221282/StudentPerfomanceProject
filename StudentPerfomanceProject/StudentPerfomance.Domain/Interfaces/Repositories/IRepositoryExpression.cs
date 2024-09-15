using System.Linq.Expressions;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Domain.Interfaces.Repositories;

public interface IRepositoryExpression<TEntity>
where TEntity : Entity
{
	Expression<Func<TEntity, bool>> Build();
}
