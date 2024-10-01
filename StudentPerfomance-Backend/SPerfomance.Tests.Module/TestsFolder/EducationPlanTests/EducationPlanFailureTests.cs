using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;
using SPerfomance.Tests.Module.TestsFolder.EducationPlanTests.TestSamples;

namespace SPerfomance.Tests.Module.TestsFolder.EducationPlanTests;

internal sealed class EducationPlanFailureTests
{
	public void CreateEducationPlanWithoutDirection()
	{
		EducationPlanSchema plan = new EducationPlanSchema(2021, null);
		new CreateEducationPlanTest(plan).DoOperation().Wait();
	}

	public void CreateEducationPlanWithYearTypeError()
	{
		EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(direction).DoOperation().Wait();
		EducationPlanSchema plan = new EducationPlanSchema(3031, direction);
		new CreateEducationPlanTest(plan).DoOperation().Wait();
		new DeleteDirectionTest(direction).DoOperation().Wait();
	}

	public void CreateEducationPlanWithYearDublicate()
	{
		EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
		new CreateDirectionTest(direction).DoOperation().Wait();
		EducationPlanSchema plan1 = new EducationPlanSchema(2021, direction);
		new CreateEducationPlanTest(plan1).DoOperation().Wait();
		EducationPlanSchema plan2 = new EducationPlanSchema(2021, direction);
		new CreateEducationPlanTest(plan2).DoOperation().Wait();
		new DeleteEducationPlanTest(plan1).DoOperation().Wait();
		new DeleteDirectionTest(direction).DoOperation().Wait();
	}
}
