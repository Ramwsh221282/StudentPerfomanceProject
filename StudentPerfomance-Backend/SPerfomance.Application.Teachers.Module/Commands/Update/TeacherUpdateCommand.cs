using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public sealed class TeacherUpdateCommand : ICommand
{
	public IRepositoryExpression<Teacher> FindInitial { get; init; }
	public IRepositoryExpression<Teacher> FindDublicate { get; init; }
	public TeacherSchema NewSchema { get; init; }
	public ISchemaValidator Validator { get; init; }
	public TeacherUpdateCommand
	(
		IRepositoryExpression<Teacher> findInitial,
		IRepositoryExpression<Teacher> findDublicate,
		TeacherSchema newSchema
	)
	{
		FindInitial = findInitial;
		FindDublicate = findDublicate;
		NewSchema = newSchema;
		Validator = new TeacherValidator()
		.WithNameValidation(newSchema)
		.WithConditionValidation(newSchema)
		.WithJobTitle(newSchema);
		Validator.ProcessValidation();
	}
}
