using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Students.Expressions;

internal sealed class StudentsFilter
(
	StudentsRepositoryObject student,
	StudentGroupsRepositoryObject group
) : IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryObject _student = student;
	private readonly StudentGroupsRepositoryObject _group = group;

	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			entity.Group.Name.Name == _group.Name &&
			(
				entity.Name.Name.Contains(_student.Name) ||
				entity.Name.Surname.Contains(_student.Surname) ||
				entity.Name.Thirdname.Contains(_student.Thirdname) ||
				entity.State.State == _student.State ||
				entity.Recordbook.Recordbook == _student.Recordbook
			);
}
