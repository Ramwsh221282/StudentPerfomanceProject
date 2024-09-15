using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Group.ChangeGroupName;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public class StudentGroupChangeDataTest(StudentsGroupSchema oldSchema, StudentsGroupSchema newSchema) : IService<StudentGroup>
{
	private readonly GroupUpdateRequest _request = new GroupUpdateRequest(oldSchema, newSchema);
	private readonly IRepository<StudentGroup> _repository = new StudentGroupsRepository();

	public async Task<OperationResult<StudentGroup>> DoOperation()
	{
		StudentGroupsRepositoryParameter oldParameter = StudentsGroupSchemaConverter
		.ToRepositoryParameter(_request.OldGroup);
		StudentGroupsRepositoryParameter newParameter = StudentsGroupSchemaConverter
		.ToRepositoryParameter(_request.NewGroup);
		IService<StudentGroup> service = new StudentGroupsUpdateService
		(
			_request.NewGroup,
			_repository,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(oldParameter),
			StudentGroupsExpressionFactory.CreateHasGroupExpression(newParameter)
		);
		OperationResult<StudentGroup> result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<StudentGroup>, StudentGroup> logger;
		logger = new(result, "Student group change data test");
		logger.ShowInfo();
		return result;
	}
}
