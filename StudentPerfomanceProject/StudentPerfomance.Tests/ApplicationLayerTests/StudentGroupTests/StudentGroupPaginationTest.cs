using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.Group.GetGroupsByPage;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupPaginationTest(int page, int pageSize) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly GroupByPageRequest _request = new GroupByPageRequest(page, pageSize);
	private readonly IRepository<StudentGroup> _repository = new StudentGroupsRepository();
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsPaginationService
		(
			_request.Page,
			_request.PageSize,
			_repository
		);
		OperationResult<IReadOnlyCollection<StudentGroup>> result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<StudentGroup>>, IReadOnlyCollection<StudentGroup>> logger =
		new(result, "Student group pagination test");
		logger.ShowInfo();
		return result;
	}
}
