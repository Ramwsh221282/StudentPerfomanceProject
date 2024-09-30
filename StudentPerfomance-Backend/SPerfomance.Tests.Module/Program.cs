using SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests;

namespace SPerfomance.Tests.Module
{
	class Program
	{
		static void Main()
		{
			var success = new EducationDirectionSuccessTests();
			//success.CreateSingleDirectionTest();
			//success.CreateMultipleDirectionTest();
			//success.UpdateDirectionFully();
			//success.UpdateDirectionCodeOnly();
			//success.UpdateDirectionNameOnly();
			//success.GetAllDirectionsTest();
			//success.GetTotalCount();
			//success.SearchyCode();
			success.SearchByType();

			//var failure = new EducationDirectionFailureTests();
			//failure.CreateEducationDirectionWithEmptyParameters();
			//failure.CreateEducationDirectionWithCodeDublicate();
			//failure.UpdateEducationDirectionWithCodeDublicate();
		}
	}
}
