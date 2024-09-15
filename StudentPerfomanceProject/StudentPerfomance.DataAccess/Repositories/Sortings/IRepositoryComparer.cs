using System.Linq.Expressions;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Repositories.Sortings;

internal interface IRepositoryComparer<TEntity, TCompareType> where TEntity : Entity
{
	public Expression<Func<TEntity, TCompareType>> BuildExpression();
}
