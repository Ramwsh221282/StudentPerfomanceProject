using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.Students.Module.Repository.Expressions;

internal sealed class GetGroup(StudentsRepositoryObject student) : IRepositoryExpression<StudentGroup>
{
	private readonly StudentsRepositoryObject _student = student;

	public Expression<Func<StudentGroup, bool>> Build() =>
		(StudentGroup entity) => entity.Name.Name == _student.Group.Name;
}
