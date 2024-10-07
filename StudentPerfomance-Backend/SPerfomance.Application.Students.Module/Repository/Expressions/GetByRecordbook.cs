using System.Linq.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Repository.Expressions;

internal sealed class GetByRecordbook(StudentsRepositoryObject student) : IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryObject _student = student;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) => entity.Recordbook.Recordbook == _student.Recordbook;
}
