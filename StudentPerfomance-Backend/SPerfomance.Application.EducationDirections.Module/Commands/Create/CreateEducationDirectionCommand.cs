using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Create;

internal sealed class CreateEducationDirectionCommand : ICommand
{
	public EducationDirectionSchema Schema { get; init; }
	public IRepositoryExpression<EducationDirection> CodeUniqueness { get; init; }
	public ISchemaValidator Validator { get; init; }

	public CreateEducationDirectionCommand
	(
		EducationDirectionSchema schema,
		IRepositoryExpression<EducationDirection> codeUniqueness
	)
	{
		Schema = schema;
		CodeUniqueness = codeUniqueness;
		Validator = new EducationDirectionValidator()
		.WithNameValidation(schema)
		.WithCodeValidator(schema)
		.WithTypeValidation(schema);
		Validator.ProcessValidation();
	}
}
