using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Students;

public sealed class HasRecordbookExpression(StudentsRepositoryParameter parameter) : IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryParameter _parameter = parameter;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) => entity.Recordbook.Recordbook == _parameter.Recordbook;
}
