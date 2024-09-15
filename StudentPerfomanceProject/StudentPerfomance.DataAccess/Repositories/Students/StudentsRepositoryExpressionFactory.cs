using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Students;

public static class StudentsRepositoryExpressionFactory
{
	public static IRepositoryExpression<Student> CreateHasStudentExpression(StudentsRepositoryParameter parameter) =>
		 new HasStudentExpression(parameter);

	public static IRepositoryExpression<Student> CreateHasRecordbookExpression(StudentsRepositoryParameter parameter) =>
		new HasRecordbookExpression(parameter);

	public static IRepositoryExpression<Student> CreateFilterExpression
	(
		StudentsRepositoryParameter student,
		StudentGroupsRepositoryParameter group
	) =>
	new StudentFilterExpression(student, group);

	public static IRepositoryExpression<Student> CreateByGroupExpression(StudentGroupsRepositoryParameter parameter) =>
		new StudentsByGroupExpression(parameter);
}
