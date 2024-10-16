using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.DeattachTeacher;

internal sealed class DeattachTeacherCommand : ICommand
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly SemesterPlanSchema _plan;
	private readonly IRepositoryExpression<Semester> _getSemester;

	public ICommandHandler<DeattachTeacherCommand, SemesterPlan> Handler;

	public DeattachTeacherCommand(SemesterPlanSchema plan)
	{
		_plan = plan;
		_getSemester = ExpressionsFactory.GetSemester(plan.Semester.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(SemesterPlansQueryRepository repository) : ICommandHandler<DeattachTeacherCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<SemesterPlan>> Handle(DeattachTeacherCommand command)
		{
			Semester? semester = await _repository.GetByParameter(command._getSemester);
			if (semester == null)
				return new OperationResult<SemesterPlan>(new SemesterNotFoundError().ToString());

			Result<SemesterPlan> plan = semester.GetContract(command._plan.Discipline);
			if (plan.IsFailure)
				return new OperationResult<SemesterPlan>(new SemesterPlanNotFoundError().ToString());

			Result deattachment = plan.Value.DeattachTeacher();
			if (deattachment.IsFailure)
				return new OperationResult<SemesterPlan>(deattachment.Error);

			await _repository.Commit();
			return new OperationResult<SemesterPlan>(plan.Value);
		}
	}
}
