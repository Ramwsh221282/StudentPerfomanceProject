using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansCountByGroupSemester;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlansCountByGroupSemesterTest(SemesterSchema semester, StudentsGroupSchema group) : IService<int>
{
	private readonly SemesterPlanCountBySemesterAndGroup _request = new SemesterPlanCountBySemesterAndGroup(semester, group);
	private readonly IRepository<SemesterPlan> _repository = new SemesterPlansRepository();
	public async Task<OperationResult<int>> DoOperation()
	{
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		StudentGroupsRepositoryParameter groupParam = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<int> service = new SemesterPlansCountByGroupSemesterService
		(
			SemesterPlansExpressionFactory.CreateByGroupAndSemesterExpression(semesterParam, groupParam),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<int>, int> logger = new(result, "Semester Plans count by Semester and Group test");
		logger.ShowInfo();
		Console.WriteLine($"Count: {result.Result}");
		return result;
	}
}
