using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public class EducationDirectionTester
{
	public class SuccessScenarios
	{
		public static SuccessScenarios CreateSucces() => new SuccessScenarios();

		public void CreateSuccess()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();
		}
	}

	public class FailureScenarios
	{
		public static FailureScenarios CreateFailure() => new FailureScenarios();

		public void CreateDublicate()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();
		}

		public void CreateWithInvalidCode()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09ds.03as.01d", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();
		}

		public void CreateWithInvalidType()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриатdsa");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();
		}
	}
}
