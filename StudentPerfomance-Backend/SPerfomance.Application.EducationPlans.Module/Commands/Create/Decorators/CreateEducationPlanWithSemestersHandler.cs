using SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators.CreationPolicies;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create.Decorators;

internal sealed class CreateEducationPlanWithSemestersHandler(
	ICommandHandler<CreateEducationPlanCommand, OperationResult<EducationPlan>> handler,
	IRepository<Semester> semesters
	) : CreateEducationPlanDecorator(handler)
{
	private readonly IRepository<Semester> _semesters = semesters;
	public override async Task<OperationResult<EducationPlan>> Handle(CreateEducationPlanCommand command)
	{
		OperationResult<EducationPlan> result = await base.Handle(command);
		if (result.Result == null || result.IsFailed) return new OperationResult<EducationPlan>(result.Error);
		ICreateEducationPlanPolicy policy = new CreateEducationPlanPolicy(result.Result, _semesters);
		await policy.ExecutePolicy();
		return result;
	}
}
