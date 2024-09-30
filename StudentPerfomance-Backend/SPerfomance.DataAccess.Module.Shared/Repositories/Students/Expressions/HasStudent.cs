using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Students.Expressions;

internal sealed class HasStudent
(
	StudentsRepositoryObject student
) : IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryObject _student = student;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			entity.Name.Name == _student.Name &&
			entity.Name.Surname == _student.Surname &&
			entity.Name.Thirdname == _student.Thirdname &&
			entity.State.State == _student.State &&
			entity.Recordbook.Recordbook == _student.Recordbook;
}
