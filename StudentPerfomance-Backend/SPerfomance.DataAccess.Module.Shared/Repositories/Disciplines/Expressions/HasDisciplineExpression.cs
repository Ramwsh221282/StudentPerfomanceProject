using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Disciplines.Expressions;

internal sealed class HasDisciplineExpression(DisciplineRepositoryObject discipline) : IRepositoryExpression<Discipline>
{
	private readonly DisciplineRepositoryObject _discipline = discipline;

	public Expression<Func<Discipline, bool>> Build() =>
		(Discipline entity) => entity.Name == _discipline.Name;
}
