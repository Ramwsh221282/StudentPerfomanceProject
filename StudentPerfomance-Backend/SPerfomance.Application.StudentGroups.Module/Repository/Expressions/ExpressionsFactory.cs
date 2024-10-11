using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Repository.Expressions;

internal sealed class ExpressionsFactory
{
	public static IRepositoryExpression<StudentGroup> GetByName(StudentGroupsRepositoryObject group) =>
		new GetByName(group);

	public static IRepositoryExpression<StudentGroup> Filter(StudentGroupsRepositoryObject group) =>
		new Filter(group);

	public static IRepositoryExpression<EducationPlan> GetPlan(EducationPlansRepositoryObject plan) =>
		new GetPlan(plan);
}
