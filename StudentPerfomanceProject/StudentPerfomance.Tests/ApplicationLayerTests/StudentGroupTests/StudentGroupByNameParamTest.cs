using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.Group.GetGroupsStartsWithSearchParam;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupByNameParamTest(StudentsGroupSchema schema) : IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly GroupByNameSearchRequest _request = new GroupByNameSearchRequest(schema);
	private readonly IRepository<StudentGroup> _repository = new StudentGroupsRepository();

	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation()
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(schema);
		IService<IReadOnlyCollection<StudentGroup>> service = new StudentGroupsSearchByNameService
		(
			_request.Group,
			_repository,
			StudentGroupsExpressionFactory.CreateSearchWithNameParamExpression(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<StudentGroup>>, IReadOnlyCollection<StudentGroup>> logger
		= new(result, "Group starts with name test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Searched groups info: ");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var group in result.Result)
			{
				Console.WriteLine(group.Name.Name);
			}
		}
		return result;
	}
}
