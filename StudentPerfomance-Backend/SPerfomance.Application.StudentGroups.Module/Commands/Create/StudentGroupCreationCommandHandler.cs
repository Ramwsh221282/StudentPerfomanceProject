using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Create;

internal sealed class StudentGroupCreationCommandHandler
(
	IRepository<StudentGroup> groups,
	IRepository<EducationPlan> plans
) : ICommandHandler<StudentGroupCreationCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _groups = groups;
	private readonly IRepository<EducationPlan> _plans = plans;
	public async Task<OperationResult<StudentGroup>> Handle(StudentGroupCreationCommand command)
	{
		if (!command.Validator.IsValid) return new OperationResult<StudentGroup>(command.Validator.Error);
		EducationPlan? plan = await _plans.GetByParameter(command.FindEducationPlan);
		if (plan == null) return new OperationResult<StudentGroup>(new EducationPlanNotFoundError().ToString());
		GroupName name = command.Group.CreateGroupName();
		StudentGroup group = StudentGroup.Create(name, plan).Value;
		await _groups.Create(group);
		return new OperationResult<StudentGroup>(group);
	}
}
