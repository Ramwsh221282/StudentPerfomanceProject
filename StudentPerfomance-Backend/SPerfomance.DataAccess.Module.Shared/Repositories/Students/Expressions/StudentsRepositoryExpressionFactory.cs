using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Students.Expressions;

public static class StudentsRepositoryExpressionFactory
{
	public static IRepositoryExpression<Student> CreateHasStudentExpression(StudentsRepositoryObject student) =>
		new HasStudent(student);

	public static IRepositoryExpression<Student> CreateHasRecordbookExpression(StudentsRepositoryObject student) =>
		new HasRecordbook(student);

	public static IRepositoryExpression<Student> CreateFilterExpression(StudentsRepositoryObject student) =>
		new StudentsFilter(student);
}
