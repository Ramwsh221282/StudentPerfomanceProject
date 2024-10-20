using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationPlans.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly EducationPlanSchema _plan;
	private readonly IRepositoryExpression<EducationDirection> _findDirection;
	private readonly ISchemaValidator _validator;
	private readonly EducationPlanCommandRepository _repository;

	public readonly ICommandHandler<CreateCommand, EducationPlan> Handler;

	public CreateCommand(EducationPlanDTO plan, string token)
	{
		_plan = plan.ToSchema();
		_findDirection = ExpressionsFactory.GetDirection(_plan.ToRepositoryObject());
		_validator = new EducationPlanValidator().WithYearValidation(_plan);
		_validator.ProcessValidation();
		_repository = new EducationPlanCommandRepository();
		Handler = new VerificationHandler<CreateCommand, EducationPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<CreateCommand, EducationPlan>
	{
		private readonly EducationPlanCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<CreateCommand, EducationPlan> handler,
			EducationPlanCommandRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<EducationPlan>> Handle(CreateCommand command)
		{
			OperationResult<EducationPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid) return new OperationResult<EducationPlan>(command._validator.Error);
			Result<EducationPlan> create = await _repository.Create(command._plan, command._findDirection);
			return create.IsFailure ?
				new OperationResult<EducationPlan>(create.Error) :
				new OperationResult<EducationPlan>(create.Value);
		}
	}
}
