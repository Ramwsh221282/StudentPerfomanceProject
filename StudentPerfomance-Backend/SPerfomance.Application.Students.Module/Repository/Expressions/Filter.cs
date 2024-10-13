using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Repository.Expressions;

internal sealed class Filter(StudentsRepositoryObject student) : IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryObject _student = student;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			(!string.IsNullOrWhiteSpace(_student.Name) && entity.Name.Name.Contains(_student.Name)) ||
			(!string.IsNullOrWhiteSpace(_student.Surname) && entity.Name.Name.Contains(_student.Surname)) ||
			(!string.IsNullOrWhiteSpace(_student.Thirdname) && entity.Name.Name.Contains(_student.Thirdname)) ||
			(!string.IsNullOrWhiteSpace(_student.State) && entity.State.State == _student.State) ||
			(_student.Recordbook == entity.Recordbook.Recordbook) &&
			!string.IsNullOrWhiteSpace(_student.Group.Name) && entity.Group.Name.Name.Contains(_student.Group.Name);
}
