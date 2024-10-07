using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Delete;

internal sealed class DeleteCommand : ICommand
{
	private readonly IRepositoryExpression<EducationPlan> _expression;
	private readonly EducationPlanCommandRepository _repository;
	public readonly ICommandHandler<DeleteCommand, EducationPlan> Handler;
	public DeleteCommand(EducationPlanSchema schema)
	{
		_expression = ExpressionsFactory.GetPlan(schema.ToRepositoryObject());
		_repository = new EducationPlanCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(EducationPlanCommandRepository repository) : ICommandHandler<DeleteCommand, EducationPlan>
	{
		private readonly EducationPlanCommandRepository _repository = repository;
		public async Task<OperationResult<EducationPlan>> Handle(DeleteCommand command)
		{
			Result<EducationPlan> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<EducationPlan>(delete.Error) :
				new OperationResult<EducationPlan>(delete.Value);
		}
	}
}
