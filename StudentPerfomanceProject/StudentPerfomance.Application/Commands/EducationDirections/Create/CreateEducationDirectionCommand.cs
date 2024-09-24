using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.EntitySchemas.Validators.EducationDirectionsValidations;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Create;

internal sealed class CreateEducationDirectionCommand : ICommand
{
	public EducationDirectionSchema Schema { get; init; }
	public IRepositoryExpression<EducationDirection> Dublicate { get; init; }
	public ISchemaValidator Validator { get; init; }

	public CreateEducationDirectionCommand
	(
		EducationDirectionSchema schema,
		IRepositoryExpression<EducationDirection> dublicate
	)
	{
		Schema = schema;
		Dublicate = dublicate;
		Validator = new EducationDirectionValidator()
		.WithNameValidation(schema)
		.WithCodeValidator(schema)
		.WithTypeValidation(schema);
		Validator.ProcessValidation();
	}
}
