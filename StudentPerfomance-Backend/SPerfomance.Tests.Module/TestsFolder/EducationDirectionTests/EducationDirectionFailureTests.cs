using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests;

internal sealed class EducationDirectionFailureTests
{
	public void CreateEducationDirectionWithEmptyParameters()
	{
		EducationDirectionSchema[] schemas = [
			new EducationDirectionSchema(),
			new EducationDirectionSchema("09.03.01", "", "Бакалавриат"),
			new EducationDirectionSchema("", "Информатика и вычислительная техника", "Бакалавриат"),
			new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "")
		];
		foreach (var schema in schemas)
		{
			new CreateDirectionTest(schema).DoOperation().Wait();
			new DeleteDirectionTest(schema).DoOperation().Wait();
		}
	}

	public void CreateEducationDirectionWithCodeDublicate()
	{
		EducationDirectionSchema[] schemas = [
			new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат"),
			new EducationDirectionSchema("09.03.01", "Программная инженерия", "Магистратура"),
		];
		foreach (var schema in schemas)
		{
			new CreateDirectionTest(schema).DoOperation().Wait();
		}
		foreach (var schema in schemas)
		{
			new DeleteDirectionTest(schema).DoOperation().Wait();
		}
	}

	public void UpdateEducationDirectionWithCodeDublicate()
	{
		EducationDirectionSchema schema1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema schema2 = new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат");
		new CreateDirectionTest(schema1).DoOperation().Wait();
		new CreateDirectionTest(schema2).DoOperation().Wait();
		new UpdateDirectionTest(schema1, schema2).DoOperation().Wait();
		new DeleteDirectionTest(schema1).DoOperation().Wait();
		new DeleteDirectionTest(schema2).DoOperation().Wait();
	}
}
