using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests;
using SPerfomance.Tests.Module.TestsFolder.EducationPlanTests;

namespace SPerfomance.Tests.Module
{
	class Program
	{
		static void Main()
		{
			var success = new EducationPlanSuccessTests();
			//success.CreateSingleBachelorPlan();
			//success.CreateMultiplePlans();
			//success.CreateGetCountEducationPlansTest();
			//success.CreateEducationDirectionSearchByYearTest();
			success.CreateEducationDirectionSearchByDirectionNameTest();


			var failure = new EducationPlanFailureTests();
			//failure.CreateEducationPlanWithYearTypeError();
			//failure.CreateEducationPlanWithYearDublicate();
		}
	}
}
