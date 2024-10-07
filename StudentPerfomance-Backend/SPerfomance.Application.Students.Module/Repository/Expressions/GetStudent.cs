using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Repository.Expressions;

internal sealed class GetStudent(StudentsRepositoryObject student) : IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryObject _student = student;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			entity.Name.Name == _student.Name &&
			entity.Name.Surname == _student.Surname &&
			entity.Name.Thirdname == _student.Thirdname &&
			entity.State.State == _student.State &&
			entity.Recordbook.Recordbook == _student.Recordbook &&
			entity.Group.Name.Name == _student.Group.Name;
}
