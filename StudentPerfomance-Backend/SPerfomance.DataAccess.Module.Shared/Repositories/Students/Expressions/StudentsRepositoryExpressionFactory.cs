using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Students.Expressions;

public static class StudentsRepositoryExpressionFactory
{
	public static IRepositoryExpression<Student> CreateHasStudentExpression(StudentsRepositoryObject parameter) =>
		new HasStudent(parameter);

	public static IRepositoryExpression<Student> CreateHasRecordbookExpression(StudentsRepositoryObject parameter) =>
		new HasRecordbook(parameter);

	public static IRepositoryExpression<Student> CreateFilterExpression(StudentsRepositoryObject student, StudentGroupsRepositoryObject group) =>
		new StudentsFilter(student, group);

	public static IRepositoryExpression<Student> CreateByGroupExpression(StudentGroupsRepositoryObject parameter) =>
		new StudentsByGroup(parameter);
}
