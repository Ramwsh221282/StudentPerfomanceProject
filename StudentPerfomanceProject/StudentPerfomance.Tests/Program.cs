using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationPlans;
using StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

namespace StudentPerfomance.Tests
{
	class Program
	{
		static void Main()
		{
			var SuccessScenarios = EducationDirectionTester.SuccessScenarios.CreateSucces();
			//SuccessScenarios.CreateSuccess();
			//SuccessScenarios.FetchFiltered();
			//SuccessScenarios.FetchFilteredByName();
			//SuccessScenarios.FetchFilteredByName();
			//SuccessScenarios.PagedFetchFiltered();
			//SuccessScenarios.PagedFetchFilteredByCode();
			//SuccessScenarios.PagedFetchFilteredByName();
			//SuccessScenarios.CountTest();
			//SuccessScenarios.FetchPaged();
			//SuccessScenarios.UpdateNameTest();
			//SuccessScenarios.UpdateCodeTest();
			//SuccessScenarios.UpdateFullTest();

			var FailureScenarios = EducationDirectionTester.FailureScenarios.CreateFailure();
			//FailureScenarios.CreateWithDublicateCode();
			//FailureScenarios.UpdateWithNotExist();
			//FailureScenarios.UpdateWithCodeDublicateExist();
		}
	}
}
