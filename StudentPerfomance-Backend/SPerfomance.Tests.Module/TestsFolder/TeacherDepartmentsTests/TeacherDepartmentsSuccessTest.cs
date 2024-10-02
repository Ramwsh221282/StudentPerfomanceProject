using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests;

internal sealed class TeacherDepartmentsSuccessTest
{
	public void CreateSingleDepartmentTest()
	{
		DepartmentSchema department = new DepartmentSchema("Институт Телекоммуникаций Систем");
		new CreateTeacherDepartmentTest(department).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department).DoOperation().Wait();
	}

	public void CreateMultipleDepartmentsTest()
	{
		DepartmentSchema department1 = new DepartmentSchema("Институт Телекоммуникаций Систем");
		DepartmentSchema department2 = new DepartmentSchema("Кафедра Гуманитарных Направлений");
		new CreateTeacherDepartmentTest(department1).DoOperation().Wait();
		new CreateTeacherDepartmentTest(department2).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department1).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department2).DoOperation().Wait();
	}

	public void CreateDepartmentsFilterByName()
	{
		DepartmentSchema department1 = new DepartmentSchema("Институт Телекоммуникационных Систем");
		DepartmentSchema department2 = new DepartmentSchema("Кафедра Гуманитарных Направлений");
		new CreateTeacherDepartmentTest(department1).DoOperation().Wait();
		new CreateTeacherDepartmentTest(department2).DoOperation().Wait();
		DepartmentSchema filter = new DepartmentSchema("Систем");
		new FilterTeacherDepartmentsTest(filter, 1, 1).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department1).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department2).DoOperation().Wait();
	}

	public void CreateDepartmentsPaginationTest()
	{
		DepartmentSchema[] schemas = [
			new DepartmentSchema("E 1"),
			new DepartmentSchema("E 2"),
			new DepartmentSchema("E 3"),
			new DepartmentSchema("E 4"),
			new DepartmentSchema("B 1"),
			new DepartmentSchema("B 2"),
			new DepartmentSchema("B 3"),
		];
		foreach (var department in schemas)
		{
			new CreateTeacherDepartmentTest(department).DoOperation().Wait();
		}
		new DepartmentPaginationTest(1, 5).DoOperation().Wait();
		foreach (var schema in schemas)
		{
			new DeleteTeacherDepartmentTest(schema).DoOperation().Wait();
		}
	}
}
