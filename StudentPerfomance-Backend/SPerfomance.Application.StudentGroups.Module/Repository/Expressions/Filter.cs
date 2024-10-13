using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Repository.Expressions;

internal sealed class Filter(StudentGroupsRepositoryObject group) : IRepositoryExpression<StudentGroup>
{
	private readonly StudentGroupsRepositoryObject _group = group;
	public Expression<Func<StudentGroup, bool>> Build() =>
		(StudentGroup entity) =>
			!string.IsNullOrWhiteSpace(_group.Name) && entity.Name.Name.Contains(_group.Name) ||

			(!string.IsNullOrWhiteSpace(_group.EducationPlan.Direction.Name) &&
			entity.EducationPlan != null &&
			entity.EducationPlan.Direction.Name.Name.Contains(_group.EducationPlan.Direction.Name)) ||

			(!string.IsNullOrWhiteSpace(_group.EducationPlan.Direction.Code) &&
			entity.EducationPlan != null &&
			entity.EducationPlan.Direction.Code.Code.Contains(_group.EducationPlan.Direction.Code)) ||

			(!string.IsNullOrWhiteSpace(_group.EducationPlan.Direction.Type) &&
			entity.EducationPlan != null &&
			entity.EducationPlan.Direction.Type.Type.Contains(_group.EducationPlan.Direction.Type)) ||

			(entity.EducationPlan != null && _group.EducationPlan.Year == entity.EducationPlan.Year.Year);
}
