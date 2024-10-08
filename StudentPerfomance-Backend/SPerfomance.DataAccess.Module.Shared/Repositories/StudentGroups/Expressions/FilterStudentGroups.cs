using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;

internal sealed class FilterStudentGroups(StudentGroupsRepositoryObject group) : IRepositoryExpression<StudentGroup>
{
	private readonly StudentGroupsRepositoryObject _group = group;
	public Expression<Func<StudentGroup, bool>> Build() =>
		(StudentGroup entity) =>
		entity.Name.Name.Contains(_group.Name) ||
		(entity.EducationPlan != null && entity.EducationPlan.Year.Year == _group.EducationPlan.Year) ||
		(entity.EducationPlan != null && entity.EducationPlan.Direction.Name.Name.Contains(_group.EducationPlan.Direction.Name)) ||
		(entity.EducationPlan != null && entity.EducationPlan.Direction.Code.Code.Contains(_group.EducationPlan.Direction.Code)) ||
		(entity.EducationPlan != null && entity.EducationPlan.Direction.Type.Type.Contains(_group.EducationPlan.Direction.Type));
}
