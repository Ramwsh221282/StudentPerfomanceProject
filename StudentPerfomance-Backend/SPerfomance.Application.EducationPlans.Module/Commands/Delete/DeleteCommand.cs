using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Delete;

internal sealed class DeleteCommand : ICommand
{
	private readonly IRepositoryExpression<EducationPlan> _expression;
	private readonly EducationPlanCommandRepository _repository;

	public readonly ICommandHandler<DeleteCommand, EducationPlan> Handler;

	public DeleteCommand(EducationPlanDTO plan, string token)
	{
		_expression = ExpressionsFactory.GetPlan(plan.ToSchema().ToRepositoryObject());
		_repository = new EducationPlanCommandRepository();
		Handler = new VerificationHandler<DeleteCommand, EducationPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<DeleteCommand, EducationPlan>
	{
		private readonly EducationPlanCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<DeleteCommand, EducationPlan> handler,
			EducationPlanCommandRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<EducationPlan>> Handle(DeleteCommand command)
		{
			OperationResult<EducationPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<EducationPlan> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<EducationPlan>(delete.Error) :
				new OperationResult<EducationPlan>(delete.Value);
		}
	}
}
