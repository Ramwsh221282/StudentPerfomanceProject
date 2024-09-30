using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests;

internal sealed class EducationDirectionSuccessTests
{
	public void CreateSingleDirectionTest()
	{
		EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(schema).DoOperation().Wait();
		new DeleteDirectionTest(schema).DoOperation().Wait();
	}

	public void CreateMultipleDirectionTest()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
	}

	public void UpdateDirectionFully()
	{
		EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(schema).DoOperation().Wait();
		EducationDirectionSchema newSchema = new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат");
		new UpdateDirectionTest(schema, newSchema).DoOperation().Wait();
		new DeleteDirectionTest(newSchema).DoOperation().Wait();
	}

	public void UpdateDirectionCodeOnly()
	{
		EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(schema).DoOperation().Wait();
		EducationDirectionSchema newSchema = new EducationDirectionSchema("09.03.04", "Информатика и вычислительная техника", "Бакалавриат");
		new UpdateDirectionTest(schema, newSchema).DoOperation().Wait();
		new DeleteDirectionTest(newSchema).DoOperation().Wait();
	}

	public void UpdateDirectionNameOnly()
	{
		EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(schema).DoOperation().Wait();
		EducationDirectionSchema newSchema = new EducationDirectionSchema("09.03.01", "Программная инженерия", "Бакалавриат");
		new UpdateDirectionTest(schema, newSchema).DoOperation().Wait();
		new DeleteDirectionTest(newSchema).DoOperation().Wait();
	}

	public void GetAllDirectionsTest()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new GetAllDirectionsTest().DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
	}

	public void GetTotalCount()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new GetTotalCountDirectionsTest().DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
	}

	public void GetPaged()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new GetPagedDirectionsTest(1, 1).DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
	}

	public void SearchyCode()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		EducationDirectionSchema schema3 = new EducationDirectionSchema("08.04.01", "Программная инженерия", "Бакалавриат");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new CreateDirectionTest(schema3).DoOperation().Wait();
		EducationDirectionSchema code = new EducationDirectionSchema("09", "", "");
		new SearchDirectionsTest(code).DoOperation().Wait();
		new GetPagedDirectionsTest(1, 1).DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
		new DeleteDirectionTest(schema3).DoOperation().Wait();
	}

	public void SearchByName()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		EducationDirectionSchema schema3 = new EducationDirectionSchema("08.04.01", "Программная инженерия", "Бакалавриат");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new CreateDirectionTest(schema3).DoOperation().Wait();
		EducationDirectionSchema name = new EducationDirectionSchema("", "Информатика", "");
		new SearchDirectionsTest(name).DoOperation().Wait();
		new GetPagedDirectionsTest(1, 1).DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
		new DeleteDirectionTest(schema3).DoOperation().Wait();
	}

	public void SearchByType()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		EducationDirectionSchema schema3 = new EducationDirectionSchema("08.04.01", "Программная инженерия", "Бакалавриат");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new CreateDirectionTest(schema3).DoOperation().Wait();
		Console.WriteLine("Type filter test");
		EducationDirectionSchema type = new EducationDirectionSchema("", "", "Магистратура");
		new SearchDirectionsTest(type).DoOperation().Wait();
		new GetPagedDirectionsTest(1, 1).DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
		new DeleteDirectionTest(schema3).DoOperation().Wait();
	}
}
