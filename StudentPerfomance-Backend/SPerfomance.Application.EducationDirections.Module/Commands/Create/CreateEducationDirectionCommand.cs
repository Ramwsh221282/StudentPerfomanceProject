using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Create;

public sealed class CreateEducationDirectionCommand : ICommand
{
	private readonly EducationDirectionSchema _schema;
	private readonly IRepositoryExpression<EducationDirection> _codeUniqueness;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<CreateEducationDirectionCommand, EducationDirection> Handler;
	public CreateEducationDirectionCommand
	(
		EducationDirectionSchema schema,
		IRepositoryExpression<EducationDirection> codeUniqueness,
		IRepository<EducationDirection> repository
	)
	{
		_schema = schema;
		_codeUniqueness = codeUniqueness;
		_validator = new EducationDirectionValidator()
		.WithNameValidation(schema)
		.WithCodeValidator(schema)
		.WithTypeValidation(schema);
		_validator.ProcessValidation();
		Handler = new CommandHandler(repository);
	}

	internal sealed class CommandHandler(IRepository<EducationDirection> repository) : ICommandHandler<CreateEducationDirectionCommand, EducationDirection>
	{
		private readonly IRepository<EducationDirection> _repository = repository;
		public async Task<OperationResult<EducationDirection>> Handle(CreateEducationDirectionCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<EducationDirection>(command._validator.Error);
			if (await _repository.HasEqualRecord(command._codeUniqueness))
				return new OperationResult<EducationDirection>(new EducationDirectionCodeDublicateError(command._schema.Code).ToString());
			EducationDirection direction = command._schema.CreateDomainObject();
			await _repository.Create(direction);
			return new OperationResult<EducationDirection>(direction);
		}
	}
}
