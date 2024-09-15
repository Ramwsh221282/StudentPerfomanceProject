using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.Group.GetGroupsByFilter;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupByFilterTest(StudentsGroupSchema schema, int page, int pageSize) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly GroupByFilterRequest _request = new GroupByFilterRequest(schema, page, pageSize);
	private readonly IRepository<StudentGroup> _repository = new StudentGroupsRepository();

	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(schema);
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsFilterService
		(
			_request.Group,
			_request.Page,
			_request.PageSize,
			_repository,
			StudentGroupsExpressionFactory.CreateFilterExpression(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<StudentGroup>>, IReadOnlyCollection<StudentGroup>> logger =
		new(result, "Student Groups by name filter");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Filtered groups: ");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var group in result.Result)
			{
				Console.WriteLine(group.Name.Name);
			}
		}
		return result;
	}
}
