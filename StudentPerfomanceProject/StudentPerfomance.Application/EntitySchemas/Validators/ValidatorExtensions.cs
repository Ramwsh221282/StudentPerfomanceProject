using StudentPerfomance.Application.Commands;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.EntitySchemas.Validators;

internal static class ValidatorExtensions
{
	public static void ValidateSchema(this ISchemaValidator validator, CommandWithErrorBuilder builder)
	{
		if (!validator.IsValid) builder.AppendError(validator.Error);
	}

	public static async Task ValidateExistance<TEntity>
	(
		this IRepository<TEntity> repository,
		IRepositoryExpression<TEntity> expression,
		string messageIfNotValid,
		CommandWithErrorBuilder builder
	)
	where TEntity : Entity
	{
		if (await repository.HasEqualRecord(expression)) builder.AppendError(messageIfNotValid);
	}

	public static void ValidateNullability(this Entity? entity, string messageIfNull, CommandWithErrorBuilder builder)
	{
		if (entity == null) builder.AppendError(messageIfNull);
	}
}
