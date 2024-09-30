using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

public interface IRepositoryExpression<TEntity> where TEntity : Entity
{
	Expression<Func<TEntity, bool>> Build();
}
