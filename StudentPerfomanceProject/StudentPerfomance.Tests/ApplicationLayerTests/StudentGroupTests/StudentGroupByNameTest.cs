using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.Group.GetGroupByName;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupByNameTest(StudentsGroupSchema schema) : IService<StudentGroup>
{
	private readonly GroupByNameRequest _request = new GroupByNameRequest(schema);
	private readonly IRepository<StudentGroup> _repository = new StudentGroupsRepository();
	public async Task<OperationResult<StudentGroup>> DoOperation()
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<StudentGroup> service = new StudentGroupByNameService
		(
			_request.Group,
			_repository,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(parameter)
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<StudentGroup>, StudentGroup> logger = new(result, "Student Group Get By Name Test");
		logger.ShowInfo();
		return result;
	}
}
