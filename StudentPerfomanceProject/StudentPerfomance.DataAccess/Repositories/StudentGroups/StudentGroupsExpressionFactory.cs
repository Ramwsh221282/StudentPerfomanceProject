using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.StudentGroups;

public static class StudentGroupsExpressionFactory
{
	public static IRepositoryExpression<StudentGroup> CreateHasGroupExpression(StudentGroupsRepositoryParameter parameter) =>
		new HasStudentGroupExpression(parameter);

	public static IRepositoryExpression<StudentGroup> CreateFilterExpression(StudentGroupsRepositoryParameter parameter) =>
		new StudentGroupsRepositoryFilterExpression(parameter);

	public static IRepositoryExpression<StudentGroup> CreateSearchWithNameParamExpression(StudentGroupsRepositoryParameter parameter) =>
		new StartsWithGroupNameExpression(parameter);
}
