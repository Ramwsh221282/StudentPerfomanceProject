using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Group.CreateStudentGroup;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupCreateTest(StudentsGroupSchema schema, EducationPlanSchema plan) : IService<StudentGroup>
{
	private readonly GroupCreateRequest _request = new GroupCreateRequest(schema, plan);
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();
	private readonly IRepository<EducationPlan> _plans = new EducationPlansRepository();
	public async Task<OperationResult<StudentGroup>> DoOperation()
	{
		StudentGroupsRepositoryParameter groupParameter = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		EducationPlanRepositoryParameter planParameter = EducationPlanSchemaConverter.ToRepositoryParameter(_request.EducationPlan);
		EducationDirectionRepositoryParameter directionParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_request.EducationPlan.Direction);
		IService<StudentGroup> service = new StudentGroupCreationService
		(
			_request.Group,
			_request.EducationPlan,
			_groups,
			_plans,
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParameter),
			EducationPlanExpressionsFactory.CreateFindPlan(planParameter, directionParameter)
		);
		OperationResult<StudentGroup> result = await service.DoOperation();
		OperationResultLogger<OperationResult<StudentGroup>, StudentGroup> logger;
		logger = new(result, "Student group creation test");
		logger.ShowInfo();
		if (result != null && result.Result != null)
		{
			Console.WriteLine($"Student Group name: {result.Result.Name.Name}");
			Console.WriteLine($"Student Group Students Count: {result.Result.Students.Count}");
		}
		return result;
	}
}
