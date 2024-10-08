using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.DeattachTeacher;

internal sealed class DeattachTeacherCommand : ICommand
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly IRepositoryExpression<SemesterPlan> _getPlan;
	public ICommandHandler<DeattachTeacherCommand, SemesterPlan> Handler;
	public DeattachTeacherCommand(SemesterPlanSchema plan)
	{
		_getPlan = ExpressionsFactory.GetPlan(plan.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(SemesterPlansQueryRepository repository) : ICommandHandler<DeattachTeacherCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<SemesterPlan>> Handle(DeattachTeacherCommand command)
		{
			SemesterPlan? plan = await _repository.GetByParameter(command._getPlan);
			if (plan == null) return new OperationResult<SemesterPlan>(new SemesterPlanNotFoundError().ToString());
			Result deattachment = plan.DeattachTeacher();
			if (deattachment.IsFailure) return new OperationResult<SemesterPlan>(deattachment.Error);
			await _repository.Commit();
			return new OperationResult<SemesterPlan>(plan);
		}
	}
}
