using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.Delete;

internal sealed class DeleteCommand : ICommand
{
	private readonly SemesterPlanSchema _plan;
	private readonly IRepositoryExpression<Semester> _getSemester;
	private readonly SemesterPlansCommandRepository _repository;

	public readonly ICommandHandler<DeleteCommand, SemesterPlan> Handler;

	public DeleteCommand(SemesterSchema semester, SemesterPlanSchema plan)
	{
		_getSemester = ExpressionsFactory.GetSemester(semester.ToRepositoryObject());
		_plan = plan;
		_repository = new SemesterPlansCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(SemesterPlansCommandRepository repository) : ICommandHandler<DeleteCommand, SemesterPlan>
	{
		private readonly SemesterPlansCommandRepository _repository = repository;

		public async Task<OperationResult<SemesterPlan>> Handle(DeleteCommand command)
		{
			Result<SemesterPlan> delete = await _repository.Remove(command._getSemester, command._plan.Discipline);
			return delete.IsFailure ?
				new OperationResult<SemesterPlan>(delete.Error) :
				new OperationResult<SemesterPlan>(delete.Value);
		}
	}
}
