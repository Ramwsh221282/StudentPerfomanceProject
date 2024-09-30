using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByFilter;
using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanFilterTest(int page, int pageSize, StudentsGroupSchema group, SemesterSchema semester, DisciplineSchema discipline)
: IService<IReadOnlyCollection<SemesterPlan>>
{
	private readonly SemesterPlanByPageAndFilter _request = new SemesterPlanByPageAndFilter(page, pageSize, group, semester, discipline);
	private readonly IRepository<SemesterPlan> _repository = new SemesterPlansRepository();

	public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> DoOperation()
	{
		StudentGroupsRepositoryParameter groupParam = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		DisciplineRepositoryParameter disciplineParam = DisciplineSchemaConverter.ToRepositoryParameter(_request.Discipline);
		IService<IReadOnlyCollection<SemesterPlan>> service = new SemesterPlansFilterService
		(
			_request.Page,
			_request.PageSize,
			SemesterPlansExpressionFactory.CreateFilterExpression(groupParam, semesterParam, disciplineParam),
			_repository
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<IReadOnlyCollection<SemesterPlan>>, IReadOnlyCollection<SemesterPlan>> logger
		= new(result, "Semester Plans Paginated Filter test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Semester Plans filter test info:");
			Console.WriteLine($"Count: {result.Result.Count}");
			foreach (var item in result.Result)
			{
				Console.WriteLine($"ID: {item.Id}");
				Console.WriteLine($"Full plan name: {item.PlanName}");
				Console.WriteLine($"Semester number: {item.LinkedSemester.Number.Value}");
				Console.WriteLine($"Linked discipline: {item.LinkedDiscipline.Name}");
				Console.WriteLine($"Attached teacher name: {item.LinkedDiscipline.Teacher.Name.Name}");
				Console.WriteLine($"Attached teacher surname: {item.LinkedDiscipline.Teacher.Name.Surname}");
				Console.WriteLine($"Attached teacher thirdname: {item.LinkedDiscipline.Teacher.Name.Thirdname}");
				Console.WriteLine($"Attached teacher department: {item.LinkedDiscipline.Teacher.Department.Name}");
			}
		}
		return result;
	}
}
