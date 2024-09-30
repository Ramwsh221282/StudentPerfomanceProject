using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Create;

public sealed class StudentGroupCreationService
(
	StudentsGroupSchema group,
	IRepositoryExpression<StudentGroup> checkGroupDublicate,
	IRepositoryExpression<EducationPlan> findEducationPlan,
	IRepository<StudentGroup> groups,
	IRepository<EducationPlan> plans

) : IService<StudentGroup>
{
	private readonly StudentGroupCreationCommand _command = new StudentGroupCreationCommand(group, checkGroupDublicate, findEducationPlan);
	private readonly StudentGroupCreationCommandHandler _handler = new StudentGroupCreationCommandHandler(groups, plans);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
