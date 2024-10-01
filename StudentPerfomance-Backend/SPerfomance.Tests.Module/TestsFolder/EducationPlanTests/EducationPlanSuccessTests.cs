using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;
using SPerfomance.Tests.Module.TestsFolder.EducationPlanTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.EducationPlanTests;

internal sealed class EducationPlanSuccessTests
{
	public void CreateSingleBachelorPlan()
	{
		EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(direction).DoOperation().Wait();
		EducationPlanSchema plan = new EducationPlanSchema(2021, direction);
		new CreateEducationPlanTest(plan).DoOperation().Wait();
		new DeleteEducationPlanTest(plan).DoOperation().Wait();
		new DeleteDirectionTest(direction).DoOperation().Wait();
	}

	public void CreateMultiplePlans()
	{
		EducationDirectionSchema direction1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		EducationDirectionSchema direction2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
		new CreateDirectionTest(direction1).DoOperation().Wait();
		new CreateDirectionTest(direction2).DoOperation().Wait();
		EducationPlanSchema plan1 = new EducationPlanSchema(2021, direction1);
		EducationPlanSchema plan2 = new EducationPlanSchema(2021, direction2);
		new CreateEducationPlanTest(plan1).DoOperation().Wait();
		new CreateEducationPlanTest(plan2).DoOperation().Wait();
		new DeleteEducationPlanTest(plan1).DoOperation().Wait();
		new DeleteEducationPlanTest(plan2).DoOperation().Wait();
		new DeleteDirectionTest(direction1).DoOperation().Wait();
		new DeleteDirectionTest(direction2).DoOperation().Wait();
	}

	public void CreateGetCountEducationPlansTest()
	{
		EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислиительная техника", "Бакалавриат");
		new CreateDirectionTest(direction).DoOperation().Wait();
		EducationPlanSchema[] plans =
		[
			new EducationPlanSchema(2021, direction),
			new EducationPlanSchema(2022, direction),
			new EducationPlanSchema(2023, direction),
			new EducationPlanSchema(2024, direction),
			new EducationPlanSchema(2025, direction),
		];
		foreach (var schema in plans)
		{
			new CreateEducationPlanTest(schema).DoOperation().Wait();
		}
		new GetCountEducationPlanTest().DoOperation().Wait();
		foreach (var schema in plans)
		{
			new DeleteEducationPlanTest(schema).DoOperation().Wait();
		}
		new DeleteDirectionTest(direction).DoOperation().Wait();
	}

	public void CreateEducationDirectionSearchByYearTest()
	{
		EducationDirectionSchema[] directions = [
			new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат"),
			new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура"),
			new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат"),
		];
		foreach (var direction in directions)
		{
			new CreateDirectionTest(direction).DoOperation().Wait();
		}
		EducationPlanSchema[] plans =
		[
			new EducationPlanSchema(2021, directions[0]),
			new EducationPlanSchema(2022, directions[0]),
			new EducationPlanSchema(2023, directions[0]),
			new EducationPlanSchema(2024, directions[0]),
			new EducationPlanSchema(2021, directions[1]),
			new EducationPlanSchema(2022, directions[1]),
			new EducationPlanSchema(2023, directions[1]),
			new EducationPlanSchema(2024, directions[1]),
			new EducationPlanSchema(2021, directions[2]),
			new EducationPlanSchema(2022, directions[2]),
			new EducationPlanSchema(2023, directions[2]),
			new EducationPlanSchema(2024, directions[2]),
		];
		foreach (var plan in plans)
		{
			new CreateEducationPlanTest(plan).DoOperation().Wait();
		}
		EducationPlanSchema filterSchema = new EducationPlanSchema(2021, null);
		new SearchEducationPlansTest(filterSchema).DoOperation().Wait();
		foreach (var plan in plans)
		{
			new DeleteEducationPlanTest(plan).DoOperation().Wait();
		}
		foreach (var direction in directions)
		{
			new DeleteDirectionTest(direction).DoOperation().Wait();
		}
	}

	public void CreateEducationDirectionSearchByDirectionNameTest()
	{
		EducationDirectionSchema[] directions = [
			new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат"),
			new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура"),
			new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат"),
		];
		foreach (var direction in directions)
		{
			new CreateDirectionTest(direction).DoOperation().Wait();
		}
		EducationPlanSchema[] plans =
		[
			new EducationPlanSchema(2021, directions[0]),
			new EducationPlanSchema(2022, directions[0]),
			new EducationPlanSchema(2023, directions[0]),
			new EducationPlanSchema(2024, directions[0]),
			new EducationPlanSchema(2021, directions[1]),
			new EducationPlanSchema(2022, directions[1]),
			new EducationPlanSchema(2023, directions[1]),
			new EducationPlanSchema(2024, directions[1]),
			new EducationPlanSchema(2021, directions[2]),
			new EducationPlanSchema(2022, directions[2]),
			new EducationPlanSchema(2023, directions[2]),
			new EducationPlanSchema(2024, directions[2]),
		];
		foreach (var plan in plans)
		{
			new CreateEducationPlanTest(plan).DoOperation().Wait();
		}
		EducationDirectionSchema filterDirection = new EducationDirectionSchema("", "Информатика", "");
		EducationPlanSchema filterSchema = new EducationPlanSchema(0, filterDirection);
		new SearchEducationPlansTest(filterSchema).DoOperation().Wait();
		foreach (var plan in plans)
		{
			new DeleteEducationPlanTest(plan).DoOperation().Wait();
		}
		foreach (var direction in directions)
		{
			new DeleteDirectionTest(direction).DoOperation().Wait();
		}
	}
}
