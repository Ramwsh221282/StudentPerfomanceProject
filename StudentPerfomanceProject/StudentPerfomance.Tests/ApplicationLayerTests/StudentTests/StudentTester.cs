using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public class StudentTester
{
	public class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();

		public void TestStudentCreation()
		{

		}

		public void TestStudentCreationWithoutThirdname()
		{

		}

		public void TestStudentDeletion()
		{

		}
		public void TestStudentDataChangeOfName()
		{

		}

		public void TestStudentDataChangeOfSurname()
		{

		}

		public void TestStudentDataChangeOfThirdname()
		{

		}
		public void TestStudentDataChangeOfState()
		{

		}
		public void TestStudentDataChangeRecordbook()
		{

		}

		public void TestStudentDataChangeGroup()
		{

		}

		public void TestStudentDataChangeEntireStudent()
		{

		}

		public void TestStudentPagination()
		{

		}

		public void TestStudentPaginationWithOtherGroup()
		{

		}

		public void TestStudentPaginationWithPageAndFilter()
		{

		}
	}

	public class FailureScenarios
	{
		public static FailureScenarios CreateFaliureScenarios() => new FailureScenarios();

		public void TryCreateDublicate()
		{

		}
		public void TryCreateWithSameRecordBook()
		{

		}
		public void TryCreateWithInvalidState()
		{

		}
	}
}
