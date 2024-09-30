using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.SemesterRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Semesters.DeleteSemester;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;

public sealed class SemesterDeletionTest(SemesterSchema semester, StudentsGroupSchema group) : IService<Semester>
{
	private readonly SemesterDeleteRequest _request = new SemesterDeleteRequest(semester, group);
	private readonly IRepository<Semester> _repository = new SemestersRepository();

	public async Task<OperationResult<Semester>> DoOperation()
	{
		throw new NotImplementedException();
	}
}
