using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.GroupRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.Group.UpdateStudentGroup;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public class StudentGroupChangeDataTest(StudentsGroupSchema oldSchema, StudentsGroupSchema newSchema) : IService<StudentGroup>
{
	private readonly GroupUpdateRequest _request = new GroupUpdateRequest(oldSchema, newSchema);
	private readonly IRepository<StudentGroup> _groups = new StudentGroupsRepository();
	private readonly IRepository<Semester> _semesters = new SemestersRepository();

	public async Task<OperationResult<StudentGroup>> DoOperation()
	{
		throw new NotImplementedException();
	}
}
