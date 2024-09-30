using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Disciplines.Expressions;

public static class DisciplineExpressionsFactory
{
	public static IRepositoryExpression<Discipline> CreateHasExpression(DisciplineRepositoryObject parameter) =>
		new HasDisciplineExpression(parameter);
}
