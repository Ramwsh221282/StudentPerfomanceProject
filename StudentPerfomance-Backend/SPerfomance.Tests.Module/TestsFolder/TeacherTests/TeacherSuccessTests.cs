using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests.TestSamples;
using SPerfomance.Tests.Module.TestsFolder.TeacherTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.TeacherTests;

internal sealed class TeacherSuccessTests
{
	public void CreateTeacherTest()
	{
		DepartmentSchema department = new DepartmentSchema("Информационно Телекомуннистические Системы");
		new CreateTeacherDepartmentTest(department).DoOperation().Wait();
		TeacherSchema teacher = new TeacherSchema("Иван", "Иванов", "Иванович", "Штатный", "Старший преподаватель", department);
		new TeacherCreationTest(teacher).DoOperation().Wait();
		new TeacherDeletionTest(teacher).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department).DoOperation().Wait();
	}

	public void UpdateTeacherTest()
	{
		DepartmentSchema department = new DepartmentSchema("Информационно Телекомуннистические Системы");
		new CreateTeacherDepartmentTest(department).DoOperation().Wait();
		TeacherSchema teacher = new TeacherSchema("Иван", "Иванов", "Иванович", "Штатный", "Старший преподаватель", department);
		new TeacherCreationTest(teacher).DoOperation().Wait();
		TeacherSchema newTeacher = new TeacherSchema("Роман", "Панов", "Евгеньевич", "Штатный", "Старший преподаватель", department);
		new TeacherUpdateTest(teacher, newTeacher).DoOperation().Wait();
		new TeacherDeletionTest(teacher).DoOperation().Wait();
		new TeacherDeletionTest(newTeacher).DoOperation().Wait();
		new DeleteTeacherDepartmentTest(department).DoOperation().Wait();
	}
}
