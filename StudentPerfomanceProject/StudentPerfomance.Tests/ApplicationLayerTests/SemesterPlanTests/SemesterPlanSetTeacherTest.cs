using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterPlanRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.SemesterPlans.SetTeacher;
using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.SemesterPlans;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanSetTeacherTest(SemesterSchema semester, DisciplineSchema discipline, TeacherSchema teacher) : IService<SemesterPlan>
{
	private readonly SemesterPlanSetTeacherRequest _request = new SemesterPlanSetTeacherRequest(teacher, semester, discipline);
	private readonly IRepository<SemesterPlan> _plans = new SemesterPlansRepository();
	private readonly IRepository<Teacher> _teachers = new TeachersRepository();

	public async Task<OperationResult<SemesterPlan>> DoOperation()
	{
		TeacherRepositoryParameter teacherParam = TeacherSchemaConverter.ToRepositoryParameter(_request.Teacher);
		SemestersRepositoryParameter semesterParam = SemesterSchemaConverter.ToRepositoryParameter(_request.Semester);
		DisciplineRepositoryParameter disciplineParam = DisciplineSchemaConverter.ToRepositoryParameter(_request.Discipline);
		IService<SemesterPlan> service = new SemesterPlanTeacherAdjustmentService
		(
			TeacherExpressionFactory.CreateByName(teacherParam),
			SemesterPlansExpressionFactory.CreateHasExpression(semesterParam, disciplineParam),
			_plans,
			_teachers
		);
		var result = service.DoOperation().Result;
		OperationResultLogger<OperationResult<SemesterPlan>, SemesterPlan> logger = new(result, "Semester Plan Set Teacher Test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Semester plan teacher adjustment info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Full plan name: {result.Result.PlanName}");
			Console.WriteLine($"Attached discipline: {result.Result.LinkedDiscipline.Name}");
			Console.WriteLine($"Semester Number: {result.Result.LinkedSemester.Number.Value}");
			Console.WriteLine($"Attached teacher name: {result.Result.LinkedDiscipline.Teacher.Name.Name}");
			Console.WriteLine($"Attached teacher surname: {result.Result.LinkedDiscipline.Teacher.Name.Surname}");
			Console.WriteLine($"Attached teacher thirdname: {result.Result.LinkedDiscipline.Teacher.Name.Thirdname}");
			Console.WriteLine($"Attached teacher department: {result.Result.LinkedDiscipline.Teacher.Department.Name}");
		}
		return result;
	}
}
