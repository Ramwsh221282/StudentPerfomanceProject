using StudentPerfomance.Api.Requests.TeacherRequests;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

public sealed class TeacherTester
{
	public sealed class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();

		public void BasicTeacherCreation()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(department).DoOperation().Wait();
			string name = "Иван";
			string surname = "Иванов";
			string thirdname = "Иванович";
			TeacherSchema teacher = new TeacherSchema(name, surname, thirdname);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
		}
		public void TeacherWithoutThirdnameCreation()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(department).DoOperation().Wait();
			string name = "Иван";
			string surname = "Иванов";
			TeacherSchema teacher = new TeacherSchema(name, surname, null);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
		}
		public void TeacherNameChangingTest()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(department).DoOperation().Wait();
			string name = "Иван";
			string surname = "Иванов";
			string thirdname = "Иванович";
			TeacherSchema teacher = new TeacherSchema(name, surname, thirdname);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			string newName = "Петр";
			string newSurname = "Петров";
			string newThirdname = "Петрович";
			TeacherSchema newTeacher = new TeacherSchema(newName, newSurname, newThirdname);
			new TeacherUpdateTest(teacher, newTeacher).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
		}

		public void TeacherSearchByDeparmtentTest()
		{
			TeacherSchema[] schemasFirst =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
			];
			TeacherSchema[] schemasSecond =
			[
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
				new TeacherSchema("Дарья", "Лебедева", "Олеговна"),
			];
			string departmentNameFirst = "Department First";
			string departmentNameSecond = "Department Second";
			string departmentNameThird = "Department Third";
			DepartmentSchema departmentFirst = new DepartmentSchema(departmentNameFirst);
			DepartmentSchema departmentSecond = new DepartmentSchema(departmentNameSecond);
			DepartmentSchema departmentThird = new DepartmentSchema(departmentNameThird);
			new DepartmentCreateTest(departmentFirst).DoOperation().Wait();
			new DepartmentCreateTest(departmentSecond).DoOperation().Wait();
			new DepartmentCreateTest(departmentThird).DoOperation().Wait();
			foreach (var schema in schemasFirst)
			{
				new TeacherCreationTest(schema, departmentFirst).DoOperation().Wait();
			}
			foreach (var schema in schemasSecond)
			{
				new TeacherCreationTest(schema, departmentSecond).DoOperation().Wait();
			}
			new TeacherByDepartmentTest(departmentFirst).DoOperation().Wait();
			new TeacherByDepartmentTest(departmentSecond).DoOperation().Wait();
			new TeacherByDepartmentTest(departmentThird).DoOperation().Wait();
			new DepartmentDeleteTest(departmentFirst).DoOperation().Wait();
			new DepartmentDeleteTest(departmentSecond).DoOperation().Wait();
			new DepartmentDeleteTest(departmentThird).DoOperation().Wait();
		}

		public void TeacherByFilterAndPageTest()
		{
			TeacherSchema[] schemasFirst =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
			];
			TeacherSchema[] schemasSecond =
			[
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
				new TeacherSchema("Дарья", "Лебедева", "Олеговна"),
			];
			string departmentNameFirst = "Department First";
			string departmentNameSecond = "Department Second";
			DepartmentSchema departmentFirst = new DepartmentSchema(departmentNameFirst);
			DepartmentSchema departmentSecond = new DepartmentSchema(departmentNameSecond);
			new DepartmentCreateTest(departmentFirst).DoOperation().Wait();
			new DepartmentCreateTest(departmentSecond).DoOperation().Wait();
			foreach (var schema in schemasFirst)
			{
				new TeacherCreationTest(schema, departmentFirst).DoOperation().Wait();
			}
			foreach (var schema in schemasSecond)
			{
				new TeacherCreationTest(schema, departmentSecond).DoOperation().Wait();
			}
			TeacherSchema filterSchema = new TeacherSchema("Иван", "", "");
			new TeacherByFilterTest(1, 14, filterSchema, departmentFirst).DoOperation().Wait();
			new DepartmentDeleteTest(departmentFirst).DoOperation().Wait();
			new DepartmentDeleteTest(departmentSecond).DoOperation().Wait();
		}
		public void TeacherPaginationByDepartmentTest()
		{
			TeacherSchema[] schemasFirst =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
			];
			TeacherSchema[] schemasSecond =
			[
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
				new TeacherSchema("Дарья", "Лебедева", "Олеговна"),
			];
			string departmentNameFirst = "Department First";
			string departmentNameSecond = "Department Second";
			DepartmentSchema departmentFirst = new DepartmentSchema(departmentNameFirst);
			DepartmentSchema departmentSecond = new DepartmentSchema(departmentNameSecond);
			new DepartmentCreateTest(departmentFirst).DoOperation().Wait();
			new DepartmentCreateTest(departmentSecond).DoOperation().Wait();
			foreach (var schema in schemasFirst)
			{
				new TeacherCreationTest(schema, departmentFirst).DoOperation().Wait();
			}
			foreach (var schema in schemasSecond)
			{
				new TeacherCreationTest(schema, departmentSecond).DoOperation().Wait();
			}
			new TeacherPaginationTest(1, 2, departmentFirst).DoOperation().Wait();
			new TeacherPaginationTest(1, 1, departmentSecond).DoOperation().Wait();
			new DepartmentDeleteTest(departmentFirst).DoOperation().Wait();
			new DepartmentDeleteTest(departmentSecond).DoOperation().Wait();
		}

		public void TeacherByDepartmentCountTest()
		{
			TeacherSchema[] schemasFirst =
			[
				new TeacherSchema("Иван", "Иванов", "Иванович"),
				new TeacherSchema("Мария", "Сидорова", "Сергеевна"),
				new TeacherSchema("Дмитрий", "Кузнецов", "Николаевич"),
			];
			TeacherSchema[] schemasSecond =
			[
				new TeacherSchema("Ольга", "Федорова", "Владимировна"),
				new TeacherSchema("Дарья", "Лебедева", "Олеговна"),
			];
			string departmentNameFirst = "Department First";
			string departmentNameSecond = "Department Second";
			DepartmentSchema departmentFirst = new DepartmentSchema(departmentNameFirst);
			DepartmentSchema departmentSecond = new DepartmentSchema(departmentNameSecond);
			new DepartmentCreateTest(departmentFirst).DoOperation().Wait();
			new DepartmentCreateTest(departmentSecond).DoOperation().Wait();
			foreach (var schema in schemasFirst)
			{
				new TeacherCreationTest(schema, departmentFirst).DoOperation().Wait();
			}
			foreach (var schema in schemasSecond)
			{
				new TeacherCreationTest(schema, departmentSecond).DoOperation().Wait();
			}
			new TeacherByDepartmentCountTest(departmentFirst).DoOperation().Wait();
			new TeacherByDepartmentCountTest(departmentSecond).DoOperation().Wait();
			new DepartmentDeleteTest(departmentFirst).DoOperation().Wait();
			new DepartmentDeleteTest(departmentSecond).DoOperation().Wait();
		}
	}

	public sealed class FailureScenarious
	{
		public static FailureScenarious CreateFailureScenarios() => new FailureScenarious();

		public void CreateWithInvalidDepartment()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			string name = "Иван";
			string surname = "Иванов";
			string thirdname = "Иванович";
			TeacherSchema teacher = new TeacherSchema(name, surname, thirdname);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
		}

		public void CreateWithInvalidTeacherName()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(department).DoOperation().Wait();
			string name = "";
			string surname = "";
			string thirdname = "Иванович";
			TeacherSchema teacher = new TeacherSchema(name, surname, thirdname);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
		}

		public void CreateWithDublicateCheck()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(department).DoOperation().Wait();
			string name = "Иван";
			string surname = "Иванов";
			string thirdname = "Иванович";
			TeacherSchema teacher = new TeacherSchema(name, surname, thirdname);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
		}

		public void CreateDublicateNameChanging()
		{
			string departmentName = "ИТК";
			DepartmentSchema department = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(department).DoOperation().Wait();
			string name = "Иван";
			string surname = "Иванов";
			string thirdname = "Иванович";
			TeacherSchema teacher = new TeacherSchema(name, surname, thirdname);
			new TeacherCreationTest(teacher, department).DoOperation().Wait();
			new TeacherUpdateTest(teacher, teacher).DoOperation().Wait();
			new DepartmentDeleteTest(department).DoOperation().Wait();
		}
	}
}
