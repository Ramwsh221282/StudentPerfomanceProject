using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;
using StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;
using StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanTester
{
	public sealed class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();

		public void SemesterPlanValidCreationTest()
		{
			StudentsGroupSchema group = new StudentsGroupSchema("БИТ 21-11");
			SemesterSchema semester = new SemesterSchema(1);
			DisciplineSchema discipline = new DisciplineSchema("Информатика");
			new StudentGroupCreateTest(group).DoOperation().Wait();
			new SemesterCreateTest(semester, group).DoOperation().Wait();
			new SemesterPlanCreateTest(semester, discipline, group).DoOperation().Wait();
			new SemesterPlanDeleteTest(semester, discipline).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void SemesterPlanTeacherAdjusmentTest()
		{
			StudentsGroupSchema group = new StudentsGroupSchema("БИТ 21-11");
			SemesterSchema semester = new SemesterSchema(1);
			DisciplineSchema discipline = new DisciplineSchema("Информатика");
			DepartmentSchema department = new DepartmentSchema("ИТК");
			TeacherSchema teacher = new TeacherSchema("Иван", "Иванов", "Иванович");
			new DepartmentCreateTest(department).DoOperation().Wait();
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new StudentGroupCreateTest(group).DoOperation().Wait();
			new SemesterCreateTest(semester, group).DoOperation().Wait();
			new SemesterPlanCreateTest(semester, discipline, group).DoOperation().Wait();
			new SemesterPlanSetTeacherTest(semester, discipline, teacher).DoOperation().Wait();
			new SemesterPlanDeleteTest(semester, discipline).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
			new SemesterDeletionTest(semester, group).DoOperation().Wait();
		}

		public void SemesterPlanFilterTest()
		{
			StudentsGroupSchema group = new StudentsGroupSchema("БИТ 21-11");
			SemesterSchema semester = new SemesterSchema(1);
			DisciplineSchema[] disciplineSchemas =
			[
				new DisciplineSchema("Информатика"),
				new DisciplineSchema("Программирование"),
				new DisciplineSchema("Английский язык"),
				new DisciplineSchema("Базы данных")
			];
			TeacherSchema[] teacherSchemas =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
			];
			DepartmentSchema department = new DepartmentSchema("ИТК");
			new DepartmentCreateTest(department).DoOperation().Wait();
			new StudentGroupCreateTest(group).DoOperation().Wait();
			new SemesterCreateTest(semester, group).DoOperation().Wait();
			for (int index = 0; index < disciplineSchemas.Length; index++)
			{
				new TeacherCreationTest(teacherSchemas[index], department).DoOperation().Wait();
				new SemesterPlanCreateTest(semester, disciplineSchemas[index], group).DoOperation().Wait();
				new SemesterPlanSetTeacherTest(semester, disciplineSchemas[index], teacherSchemas[index]).DoOperation().Wait();
			}
			DisciplineSchema filterDiscipline = new DisciplineSchema("Прог");
			new SemesterPlanFilterTest(1, 3, group, semester, filterDiscipline).DoOperation().Wait();
			new SemesterDeletionTest(semester, group).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void SemesterPlansPaginationTest()
		{
			StudentsGroupSchema group = new StudentsGroupSchema("БИТ 21-11");
			SemesterSchema semester = new SemesterSchema(1);
			DisciplineSchema[] disciplineSchemas =
			[
				new DisciplineSchema("Информатика"),
				new DisciplineSchema("Программирование"),
				new DisciplineSchema("Английский язык"),
				new DisciplineSchema("Базы данных")
			];
			TeacherSchema[] teacherSchemas =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
			];
			DepartmentSchema department = new DepartmentSchema("ИТК");
			new DepartmentCreateTest(department).DoOperation().Wait();
			new StudentGroupCreateTest(group).DoOperation().Wait();
			new SemesterCreateTest(semester, group).DoOperation().Wait();
			for (int index = 0; index < disciplineSchemas.Length; index++)
			{
				new TeacherCreationTest(teacherSchemas[index], department).DoOperation().Wait();
				new SemesterPlanCreateTest(semester, disciplineSchemas[index], group).DoOperation().Wait();
				new SemesterPlanSetTeacherTest(semester, disciplineSchemas[index], teacherSchemas[index]).DoOperation().Wait();
			}
			new SemesterPlanPaginationTest(1, 2, semester, group).DoOperation().Wait();
			new SemesterDeletionTest(semester, group).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void SemesterPlansPaginationCountTest()
		{
			StudentsGroupSchema group = new StudentsGroupSchema("БИТ 21-11");
			SemesterSchema semester = new SemesterSchema(1);
			DisciplineSchema[] disciplineSchemas =
			[
				new DisciplineSchema("Информатика"),
				new DisciplineSchema("Программирование"),
				new DisciplineSchema("Английский язык"),
				new DisciplineSchema("Базы данных")
			];
			TeacherSchema[] teacherSchemas =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
			];
			DepartmentSchema department = new DepartmentSchema("ИТК");
			new DepartmentCreateTest(department).DoOperation().Wait();
			new StudentGroupCreateTest(group).DoOperation().Wait();
			new SemesterCreateTest(semester, group).DoOperation().Wait();
			for (int index = 0; index < disciplineSchemas.Length; index++)
			{
				new TeacherCreationTest(teacherSchemas[index], department).DoOperation().Wait();
				new SemesterPlanCreateTest(semester, disciplineSchemas[index], group).DoOperation().Wait();
				new SemesterPlanSetTeacherTest(semester, disciplineSchemas[index], teacherSchemas[index]).DoOperation().Wait();
			}
			new SemesterPlansCountByGroupSemesterTest(semester, group).DoOperation().Wait();
			new SemesterDeletionTest(semester, group).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
	}
	public sealed class FailureScenarios
	{
		public static FailureScenarios CreateFailureScenarios() => new FailureScenarios();
	}
}
