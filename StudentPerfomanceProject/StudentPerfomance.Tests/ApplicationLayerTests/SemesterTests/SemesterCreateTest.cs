using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Semesters.CreateSemester;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;

public sealed class SemesterCreateTest(SemesterSchema semester, StudentsGroupSchema group) : IService<Semester>
{
	private readonly SemesterCreateRequest _request = new SemesterCreateRequest(semester, group);
	private readonly IRepository<Semester> _semesters = new SemestersRepository();
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();

	public async Task<OperationResult<Semester>> DoOperation()
	{
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		StudentGroupsRepositoryParameter groupParam = StudentsGroupSchemaConverter.ToRepositoryParameter(_request.Group);
		IService<Semester> service = new SemestersCreationService
		(
			_request.Semester,
			SemesterExpressionFactory.CreateHasSemesterExpression(semesterParam, groupParam),
			StudentGroupsExpressionFactory.CreateHasGroupExpression(groupParam),
			_semesters,
			_groups
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<Semester>, Semester> logger = new(result, "Semester creation test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Created semester info: ");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Semester number: {result.Result.Number.Value}");
			Console.WriteLine($"Semester of group: {result.Result.Group.Name.Name}");
		}
		return result;
	}
}
