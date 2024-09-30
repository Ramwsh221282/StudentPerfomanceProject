using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

internal interface IRepositoryComparer<TEntity, TCompareType> where TEntity : Entity
{
	public Expression<Func<TEntity, TCompareType>> BuildExpression();
}
