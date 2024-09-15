using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Students;

public sealed class HasStudentExpression(StudentsRepositoryParameter parameter)
: IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryParameter _parameter = parameter;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			entity.Name.Name == _parameter.Name &&
			entity.Name.Surname == _parameter.Surname &&
			entity.Name.Thirdname == _parameter.Thirdname &&
			entity.State.State == _parameter.State &&
			entity.Recordbook.Recordbook == _parameter.Recordbook;
}
