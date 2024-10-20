using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans.Validators;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.Create;

internal sealed class CreateCommand : ICommand
{
	private readonly SemesterPlanSchema _plan;
	private readonly IRepositoryExpression<Semester> _getSemester;
	private readonly SemesterPlansCommandRepository _repository;
	private readonly ISchemaValidator _validator;

	public readonly ICommandHandler<CreateCommand, SemesterPlan> Handler;

	public CreateCommand(SemesterSchema semester, SemesterPlanSchema plan, string token)
	{
		_plan = plan;
		_getSemester = ExpressionsFactory.GetSemester(semester.ToRepositoryObject());
		_repository = new SemesterPlansCommandRepository();
		_validator = new SemesterPlanValidator()
		.WithDisciplineValidation(plan);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<CreateCommand, SemesterPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<CreateCommand, SemesterPlan>
	{
		private readonly SemesterPlansCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<CreateCommand, SemesterPlan> handler,
			SemesterPlansCommandRepository repository
			)
			: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<SemesterPlan>> Handle(CreateCommand command)
		{
			OperationResult<SemesterPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid)
				return new OperationResult<SemesterPlan>(command._validator.Error);

			Result<SemesterPlan> create = await _repository.Create(command._plan, command._getSemester);
			return create.IsFailure ?
				new OperationResult<SemesterPlan>(create.Error) :
				new OperationResult<SemesterPlan>(create.Value);
		}
	}
}
