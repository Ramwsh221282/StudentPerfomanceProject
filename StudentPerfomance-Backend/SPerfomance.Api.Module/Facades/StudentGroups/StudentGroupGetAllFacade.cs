using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Queries.All;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupGetAllFacade : IFacade<IReadOnlyCollection<StudentGroupResponse>>
{
	public async Task<ActionResult<IReadOnlyCollection<StudentGroupResponse>>> Process()
	{
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupGetAllService(RepositoryProvider.CreateStudentGroupsRepository());
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}
