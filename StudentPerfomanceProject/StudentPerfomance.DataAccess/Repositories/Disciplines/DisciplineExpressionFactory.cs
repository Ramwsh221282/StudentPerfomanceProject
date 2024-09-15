using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Disciplines;

public static class DisciplineExpressionFactory
{
	public static IRepositoryExpression<Discipline> CreateHasExpression(DisciplineRepositoryParameter parameter) =>
		new HasDisciplineExpression(parameter);
}
