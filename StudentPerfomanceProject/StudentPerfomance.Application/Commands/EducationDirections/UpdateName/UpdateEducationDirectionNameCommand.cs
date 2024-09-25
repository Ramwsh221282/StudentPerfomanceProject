using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.EntitySchemas.Validators.EducationDirectionsValidations;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.UpdateName;

internal sealed class UpdateEducationDirectionNameCommand : ICommand
{
	public EducationDirectionSchema Direction { get; init; }
	public ISchemaValidator Validator { get; init; }
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; }

	public UpdateEducationDirectionNameCommand(EducationDirectionSchema direction, IRepositoryExpression<EducationDirection> findDirection)
	{
		Direction = direction;
		FindDirection = findDirection;
		Validator = new EducationDirectionValidator()
		.WithNameValidation(direction)
		.WithCodeValidator(direction)
		.WithTypeValidation(direction);
		Validator.ProcessValidation();
	}
}
