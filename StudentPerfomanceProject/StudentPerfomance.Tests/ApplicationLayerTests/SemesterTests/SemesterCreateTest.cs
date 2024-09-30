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
		throw new NotImplementedException();
	}
}
