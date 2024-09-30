using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Update;

public sealed class StudentGroupUpdateService
(
	StudentsGroupSchema newSchema,
	IRepositoryExpression<StudentGroup> findInitialGroup,
	IRepositoryExpression<StudentGroup> findNameDublicate,
	IRepository<StudentGroup> repository
) : IService<StudentGroup>
{
	private readonly StudentGroupUpdateCommand _command = new StudentGroupUpdateCommand(newSchema, findInitialGroup, findNameDublicate);
	private readonly StudentGroupUpdateCommandHandler _handler = new StudentGroupUpdateCommandHandler(repository);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
