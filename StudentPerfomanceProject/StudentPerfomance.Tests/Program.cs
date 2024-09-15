using StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

namespace StudentPerfomance.Tests
{
	class Program
	{
		static void Main()
		{
			var successScenarios = SemesterPlanTester.SuccessScenarios.CreateSuccessScenarios();
			successScenarios.SemesterPlansPaginationCountTest();
		}
	}
}
