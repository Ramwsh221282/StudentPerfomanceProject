using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Update;

public sealed class StudentGroupUpdateCommand : ICommand
{
	private readonly StudentsGroupSchema _newSchema;
	private readonly IRepositoryExpression<StudentGroup> _findInitialGroup;
	private readonly IRepositoryExpression<StudentGroup> _findNameDublicate;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<StudentGroupUpdateCommand, StudentGroup> Handler;
	public StudentGroupUpdateCommand
	(
		StudentsGroupSchema newSchema,
		IRepositoryExpression<StudentGroup> findInitialGroup,
		IRepositoryExpression<StudentGroup> findNameDublicate,
		IRepository<StudentGroup> repository
	)
	{
		_newSchema = newSchema;
		_findInitialGroup = findInitialGroup;
		_findNameDublicate = findNameDublicate;
		_validator = new StudentGroupSchemaValidator().WithNameValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new CommandHandler(repository);
	}

	internal sealed class CommandHandler(IRepository<StudentGroup> repository) : ICommandHandler<StudentGroupUpdateCommand, StudentGroup>
	{
		private readonly IRepository<StudentGroup> _repository = repository;
		public async Task<OperationResult<StudentGroup>> Handle(StudentGroupUpdateCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<StudentGroup>(command._validator.Error);
			if (await _repository.HasEqualRecord(command._findNameDublicate))
				return new OperationResult<StudentGroup>(new GroupDublicateNameError(command._newSchema.NameInfo).ToString());
			StudentGroup? group = await _repository.GetByParameter(command._findInitialGroup);
			if (group == null)
				return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());
			GroupName updatedName = command._newSchema.CreateGroupName();
			group.ChangeGroupName(updatedName);
			return new OperationResult<StudentGroup>(group);
		}
	}
}
