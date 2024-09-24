using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Tests.ApplicationLayerTests.DepartmentTests;
using StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;
using StudentPerfomance.Tests.ApplicationLayerTests.TeacherTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterPlanTests;

public sealed class SemesterPlanTester
{
	public sealed class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();

		public void SemesterPlanValidCreationTest()
		{

		}

		public void SemesterPlanTeacherAdjusmentTest()
		{

		}

		public void SemesterPlanFilterTest()
		{

		}
		public void SemesterPlansPaginationTest()
		{

		}
		public void SemesterPlansPaginationCountTest()
		{

		}
	}
	public sealed class FailureScenarios
	{
		public static FailureScenarios CreateFailureScenarios() => new FailureScenarios();
	}
}
