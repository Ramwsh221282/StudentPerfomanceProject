using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests;
using SPerfomance.Tests.Module.TestsFolder.EducationPlanTests;
using SPerfomance.Tests.Module.TestsFolder.TeacherDepartmentsTests;
using SPerfomance.Tests.Module.TestsFolder.TeacherTests;

namespace SPerfomance.Tests.Module
{
	class Program
	{
		static void Main()
		{
			var success = new TeacherSuccessTests();
			//success.CreateTeacherTest();
			success.UpdateTeacherTest();
		}
	}
}
