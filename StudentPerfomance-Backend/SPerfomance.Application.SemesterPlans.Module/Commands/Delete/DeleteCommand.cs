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
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.Delete;

internal sealed class DeleteCommand : ICommand
{
	private readonly SemesterPlanSchema _plan;
	private readonly IRepositoryExpression<Semester> _getSemester;
	private readonly SemesterPlansCommandRepository _repository;

	public readonly ICommandHandler<DeleteCommand, SemesterPlan> Handler;

	public DeleteCommand(SemesterSchema semester, SemesterPlanSchema plan, string token)
	{
		_getSemester = ExpressionsFactory.GetSemester(semester.ToRepositoryObject());
		_plan = plan;
		_repository = new SemesterPlansCommandRepository();
		Handler = new VerificationHandler<DeleteCommand, SemesterPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<DeleteCommand, SemesterPlan>
	{
		private readonly SemesterPlansCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<DeleteCommand, SemesterPlan> handler,
			SemesterPlansCommandRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<SemesterPlan>> Handle(DeleteCommand command)
		{
			OperationResult<SemesterPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<SemesterPlan> delete = await _repository.Remove(command._getSemester, command._plan.Discipline);
			return delete.IsFailure ?
				new OperationResult<SemesterPlan>(delete.Error) :
				new OperationResult<SemesterPlan>(delete.Value);
		}
	}
}
