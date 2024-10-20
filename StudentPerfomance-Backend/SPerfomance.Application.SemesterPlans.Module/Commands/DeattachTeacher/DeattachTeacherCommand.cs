using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.DeattachTeacher;

internal sealed class DeattachTeacherCommand : ICommand
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly SemesterPlanSchema _plan;
	private readonly IRepositoryExpression<Semester> _getSemester;

	public ICommandHandler<DeattachTeacherCommand, SemesterPlan> Handler;

	public DeattachTeacherCommand(SemesterPlanSchema plan, string token)
	{
		_plan = plan;
		_getSemester = ExpressionsFactory.GetSemester(plan.Semester.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new VerificationHandler<DeattachTeacherCommand, SemesterPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<DeattachTeacherCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository;

		public CommandHandler(
			ICommandHandler<DeattachTeacherCommand, SemesterPlan> handler,
			SemesterPlansQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<SemesterPlan>> Handle(DeattachTeacherCommand command)
		{
			OperationResult<SemesterPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

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
