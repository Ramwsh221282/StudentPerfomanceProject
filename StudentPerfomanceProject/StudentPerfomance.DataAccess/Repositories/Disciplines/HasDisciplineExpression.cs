using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Disciplines;

public sealed class HasDisciplineExpression(DisciplineRepositoryParameter parameter) : IRepositoryExpression<Discipline>
{
	private readonly DisciplineRepositoryParameter _parameter = parameter;

	public Expression<Func<Discipline, bool>> Build() =>
		(Discipline entity) =>
			!string.IsNullOrWhiteSpace(_parameter.Name) && entity.Name == _parameter.Name;
}
