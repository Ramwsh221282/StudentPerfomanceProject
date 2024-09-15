using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.Queries.Departments.GetDepartmentsByPage;

namespace StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;

public sealed class DepartmentTester
{
	public sealed class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();

		public void TestDepartmentCreation()
		{
			string departmentName = "ИТК";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(schema).DoOperation().Wait();
			new DepartmentDeleteTest(schema).DoOperation().Wait();
		}

		public void TestDepartmentDeletion()
		{
			string departmentName = "ИТК";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(schema).DoOperation().Wait();
			new DepartmentDeleteTest(schema).DoOperation().Wait();
		}

		public void TestDepartmentNameChanging()
		{
			string departmentName = "ИТК";
			string newDepartmentName = "КТИ";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			DepartmentSchema newSchema = new DepartmentSchema(newDepartmentName);
			new DepartmentCreateTest(schema).DoOperation().Wait();
			new DepartmentNameChangeTest(schema, newSchema).DoOperation().Wait();
			new DepartmentDeleteTest(newSchema).DoOperation().Wait();
		}
		public void TestDepartmentPagination()
		{
			DepartmentSchema[] schemas =
			[
				new DepartmentSchema("А"),
				new DepartmentSchema("Б"),
				new DepartmentSchema("В"),
				new DepartmentSchema("Г"),
				new DepartmentSchema("Д"),
				new DepartmentSchema("Е"),
				new DepartmentSchema("Ж"),
				new DepartmentSchema("З"),
				new DepartmentSchema("И"),
				new DepartmentSchema("К"),
				new DepartmentSchema("Л"),
				new DepartmentSchema("М"),
				new DepartmentSchema("Н"),
				new DepartmentSchema("П"),
			];
			foreach (var schema in schemas)
			{
				new DepartmentCreateTest(schema).DoOperation().Wait();
			}
			int page = 1, pageSize = 14;
			new DepartmentByPageTest(page, pageSize).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				new DepartmentDeleteTest(schema).DoOperation().Wait();
			}
		}
		public void TestDepartmentSearch()
		{
			DepartmentSchema[] schemas =
			[
				new DepartmentSchema("А"),
				new DepartmentSchema("Б"),
				new DepartmentSchema("В"),
				new DepartmentSchema("Г"),
				new DepartmentSchema("Д"),
				new DepartmentSchema("Е"),
				new DepartmentSchema("Ж"),
				new DepartmentSchema("З"),
				new DepartmentSchema("И"),
				new DepartmentSchema("К"),
				new DepartmentSchema("Л"),
				new DepartmentSchema("М"),
				new DepartmentSchema("Н"),
				new DepartmentSchema("П"),
				new DepartmentSchema("Абдубдеф"),
			];
			foreach (var schema in schemas)
			{
				new DepartmentCreateTest(schema).DoOperation().Wait();
			}
			DepartmentSchema param = new DepartmentSchema("А");
			new DepartmentByFilterTest(param).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				new DepartmentDeleteTest(schema).DoOperation().Wait();
			}
		}

		public void TestFilterAndPagination()
		{
			DepartmentSchema[] schemas =
			[
				new DepartmentSchema("А"),
				new DepartmentSchema("Б"),
				new DepartmentSchema("В"),
				new DepartmentSchema("Г"),
				new DepartmentSchema("Д"),
				new DepartmentSchema("Е"),
				new DepartmentSchema("Ж"),
				new DepartmentSchema("З"),
				new DepartmentSchema("И"),
				new DepartmentSchema("К"),
				new DepartmentSchema("Л"),
				new DepartmentSchema("М"),
				new DepartmentSchema("Н"),
				new DepartmentSchema("П"),
				new DepartmentSchema("Абдубдеф"),
			];
			foreach (var schema in schemas)
			{
				new DepartmentCreateTest(schema).DoOperation().Wait();
			}
			DepartmentSchema param = new DepartmentSchema("А");
			new DepartmentByFIlterAndPageTest(1, 14, param).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				new DepartmentDeleteTest(schema).DoOperation().Wait();
			}
		}
	}

	public sealed class FailureScenarios
	{
		public static FailureScenarios CreateFaliureScenarios() => new FailureScenarios();

		public void CreateWithDublicate()
		{
			string departmentName = "ИТК";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(schema).DoOperation().Wait();
			new DepartmentCreateTest(schema).DoOperation().Wait();
			new DepartmentDeleteTest(schema).DoOperation().Wait();
		}
		public void CreateWithInvalidName()
		{
			string departmentName = "";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			new DepartmentCreateTest(schema).DoOperation().Wait();
		}
		public void DeleteWithInvalidName()
		{
			string departmentName = "";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			new DepartmentDeleteTest(schema).DoOperation().Wait();
		}
		public void DeleteWithNotExisted()
		{
			string departmentName = "Some Random Name";
			DepartmentSchema schema = new DepartmentSchema(departmentName);
			new DepartmentDeleteTest(schema).DoOperation().Wait();
		}
	}
}
