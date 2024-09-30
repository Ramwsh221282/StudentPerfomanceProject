using StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators.CreateEducationPlanPolicies;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationPlans.Create.Decorators;

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
		ICreateEducationPlanPolicy policy = new CreateEducationPlanPolicyManager(result.Result, _semesters).CreatePolicy();
		await policy.ExecutePolicy();
		return result;
	}
}
