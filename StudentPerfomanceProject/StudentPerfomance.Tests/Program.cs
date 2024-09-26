using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationPlans;
using StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;
using StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

namespace StudentPerfomance.Tests
{
	class Program
	{
		static void Main()
		{
			var success = EducationPlanTester.SuccessScenarios.BuildSuccess();
			//success.CreatePlanTest();
			//success.CreateFewPlansTest();
			//success.CountTest();
			//success.GetAllTest();
			//success.GetPagedTest();
			//success.GetFilteredTest();
			//success.GetFilteredByYearTest();
			//success.GetFilteredByDirection();

			var failure = EducationPlanTester.FailureScenarios.BuildFailure();
			//failure.CreateWithoutDirection();
			//failure.CreateWithDublication();
		}
	}
}
