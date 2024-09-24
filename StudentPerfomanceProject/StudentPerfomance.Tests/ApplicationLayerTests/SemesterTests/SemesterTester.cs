using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;

public sealed class SemesterTester
{
	public sealed class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScnearios() => new SuccessScenarios();

		public void SemesterValidCreationTest()
		{

		}

		public void FewSemestersValidCreationTest()
		{

		}
		public void SemesterFilterTest()
		{

		}

		public void SemesterPaginationTest()
		{

		}
	}

	public sealed class FailureScenarious
	{
		public static FailureScenarious CreateFailureScenarios() => new FailureScenarious();

		public void SemesterInvalidCreationTest()
		{

		}

		public void SemesterDublicateTest()
		{

		}
	}
}
