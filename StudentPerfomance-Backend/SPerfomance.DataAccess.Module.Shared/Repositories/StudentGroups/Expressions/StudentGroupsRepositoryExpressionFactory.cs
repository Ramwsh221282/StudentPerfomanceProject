using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;

public static class StudentGroupsRepositoryExpressionFactory
{
	public static IRepositoryExpression<StudentGroup> CreateHasGroupExpression(StudentGroupsRepositoryObject parameter) =>
			new FindStudentGroup(parameter);

	public static IRepositoryExpression<StudentGroup> CreateFilterExpression(StudentGroupsRepositoryObject parameter) =>
		new FilterStudentGroups(parameter);

	public static IRepositoryExpression<StudentGroup> CreateFindByNameExpression(StudentGroupsRepositoryObject parameter) =>
		new FindStudentGroupByName(parameter);
}
