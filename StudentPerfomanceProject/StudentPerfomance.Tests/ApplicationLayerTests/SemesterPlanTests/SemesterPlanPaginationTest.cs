using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByPage;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanPaginationTest(int page, int pageSize, SemesterSchema semester, StudentsGroupSchema group)
: IService<IReadOnlyCollection<SemesterPlan>>
{
	private readonly SemesterPlanByPageAndGroupSemesterRequest _request = new SemesterPlanByPageAndGroupSemesterRequest(page, pageSize, semester, group);
	private readonly IRepository<SemesterPlan> _repository = new SemesterPlansRepository();

	public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> DoOperation()
	{
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		StudentGroupsRepositoryParameter groupParam = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<IReadOnlyCollection<SemesterPlan>> service = new SemesterPlansPaginationService
		(
			_request.Page,
			_request.PageSize,
			SemesterPlansExpressionFactory.CreateByGroupAndSemesterExpression(semesterParam, groupParam),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<SemesterPlan>>, IReadOnlyCollection<SemesterPlan>> logger
		= new(result, "Semester Plans pagination test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Semester Plans pagination results");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var item in result.Result)
			{
				Console.WriteLine($"Item ID: {item.Id}");
				Console.WriteLine($"Full plan name: {item.PlanName}");
				Console.WriteLine($"Item semester number: {item.LinkedSemester.Number.Value}");
				Console.WriteLine($"Semester of group: {item.LinkedSemester.Group.Name.Name}");
			}
		}
		return result;
	}
}
