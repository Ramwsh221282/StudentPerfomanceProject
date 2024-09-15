using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Group.DeleteStudentGroup;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupDeleteTest(StudentsGroupSchema schema) : IService<StudentGroup>
{
	private readonly GroupDeleteRequest _request = new GroupDeleteRequest(schema);
	private readonly IRepository<StudentGroup> _repository = new StudentGroupsRepository();
	public async Task<OperationResult<StudentGroup>> DoOperation()
	{
		StudentGroupsRepositoryParameter parameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<StudentGroup> service = new StudentGroupsDeleteService
		(
			_request.Group,
			_repository,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(parameter)
		);
		OperationResult<StudentGroup> result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<StudentGroup>, StudentGroup> logger;
		logger = new(result, "Student group deletion test");
		logger.ShowInfo();
		return result;
	}
}
