using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<Student> GetByRecordbook(StudentsRepositoryObject student) =>
		new GetByRecordbook(student);

	public static IRepositoryExpression<StudentGroup> GetGroupByName(StudentsRepositoryObject student) =>
		new GetGroup(student);

	public static IRepositoryExpression<Student> GetStudent(StudentsRepositoryObject student) =>
		new GetStudent(student);

	public static IRepositoryExpression<Student> Filter(StudentsRepositoryObject student) =>
		new Filter(student);

	public static IRepositoryExpression<Student> GetByGroup(StudentGroupsRepositoryObject group) =>
		new GroupStudentsExpression(group);
}
