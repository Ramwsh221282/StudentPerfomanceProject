using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests;

internal sealed class TeacherDepartmentsFailureTests
{
	public void CreateDepartmentWithEmptyNameTest()
	{
		DepartmentSchema schema = new DepartmentSchema("");
		new CreateTeacherDepartmentTest(schema).DoOperation().Wait();
	}

	public void CreateDepartmentWithNameLengthError()
	{
		DepartmentSchema schema = new DepartmentSchema("ЫВФвыфвфывыфвыфвыфвыфвыфвыфвыфвыфвыфвыфвыфвфывыфвыфвЫВФвыфвфывыфвыфвыфвыфвыфвыфвыфвыфвыфвыфвыфвфывыфвыфвЫВФвыфвфывыфвыфвыфвыфвыфвыфвыфвыфвыфвыфвыфвфывыфвыфв");
		new CreateTeacherDepartmentTest(schema).DoOperation().Wait();
	}
}
