using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationPlansTests;

public sealed class EducationPlanTester
{
	public class SuccessScenarios
	{
		public static SuccessScenarios BuildSuccess() => new SuccessScenarios();

		public void CreatePlanTest()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema planSchema = new EducationPlanSchema(2021, direction);
			EducationPlanGeneralRequest request = new EducationPlanGeneralRequest(planSchema);
			new CreateEducationPlanTest(request).DoOperation().Wait();
			new DeleteEducationPlanTest(request).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}

		public void CreatePlanWithSemestersTest()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema planSchema = new EducationPlanSchema(2021, direction);
			EducationPlanGeneralRequest request = new EducationPlanGeneralRequest(planSchema);
			new CreateEducationPlanWithSemestersTest(request).DoOperation().Wait();
			new DeleteEducationPlanTest(request).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}

		public void CreatePlanWithFewDirectionsTest()
		{
			EducationDirectionSchema direction1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			EducationDirectionSchema direction2 = new EducationDirectionSchema("09.04.01", "Информатика и вычислительная техника", "Магистратура");
			CreateEducationDirectionRequest createDirectionRequest1 = new CreateEducationDirectionRequest(direction1);
			CreateEducationDirectionRequest createDirectionRequest2 = new CreateEducationDirectionRequest(direction2);
			new CreateEducationDirectionTest(createDirectionRequest1).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest2).DoOperation().Wait();
			EducationPlanSchema plan1 = new EducationPlanSchema(2021, direction1);
			EducationPlanSchema plan2 = new EducationPlanSchema(2021, direction2);
			EducationPlanGeneralRequest request1 = new EducationPlanGeneralRequest(plan1);
			EducationPlanGeneralRequest request2 = new EducationPlanGeneralRequest(plan2);
			new CreateEducationPlanWithSemestersTest(request1).DoOperation().Wait();
			new CreateEducationPlanWithSemestersTest(request2).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest1 = new DeleteEducationDirectionRequest(direction1);
			DeleteEducationDirectionRequest deleteDirectionRequest2 = new DeleteEducationDirectionRequest(direction2);
			new DeleteEducationDirectionTest(deleteDirectionRequest1).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest2).DoOperation().Wait();
		}

		public void CreateFewPlansTest()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema planSchema1 = new EducationPlanSchema(2021, direction);
			EducationPlanSchema planSchema2 = new EducationPlanSchema(2022, direction);
			EducationPlanGeneralRequest request1 = new EducationPlanGeneralRequest(planSchema1);
			EducationPlanGeneralRequest request2 = new EducationPlanGeneralRequest(planSchema2);
			new CreateEducationPlanTest(request1).DoOperation().Wait();
			new CreateEducationPlanTest(request2).DoOperation().Wait();
			new DeleteEducationPlanTest(request1).DoOperation().Wait();
			new DeleteEducationPlanTest(request2).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}

		public void CountTest()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema planSchema1 = new EducationPlanSchema(2021, direction);
			EducationPlanSchema planSchema2 = new EducationPlanSchema(2022, direction);
			EducationPlanGeneralRequest request1 = new EducationPlanGeneralRequest(planSchema1);
			EducationPlanGeneralRequest request2 = new EducationPlanGeneralRequest(planSchema2);
			new CreateEducationPlanTest(request1).DoOperation().Wait();
			new CreateEducationPlanTest(request2).DoOperation().Wait();
			new CountEducationPlanTest().DoOperation().Wait();
			new DeleteEducationPlanTest(request1).DoOperation().Wait();
			new DeleteEducationPlanTest(request2).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}

		public void GetAllTest()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema planSchema1 = new EducationPlanSchema(2021, direction);
			EducationPlanSchema planSchema2 = new EducationPlanSchema(2022, direction);
			EducationPlanGeneralRequest request1 = new EducationPlanGeneralRequest(planSchema1);
			EducationPlanGeneralRequest request2 = new EducationPlanGeneralRequest(planSchema2);
			new CreateEducationPlanTest(request1).DoOperation().Wait();
			new CreateEducationPlanTest(request2).DoOperation().Wait();
			new GetAllEducationPlanTest().DoOperation().Wait();
			new DeleteEducationPlanTest(request1).DoOperation().Wait();
			new DeleteEducationPlanTest(request2).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}

		public void GetPagedTest()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema[] schemas = [
				new EducationPlanSchema(2021, direction),
				new EducationPlanSchema(2022, direction),
				new EducationPlanSchema(2023, direction),
				new EducationPlanSchema(2024, direction),
				new EducationPlanSchema(2025, direction),
				new EducationPlanSchema(2026, direction),
				new EducationPlanSchema(2027, direction),
				new EducationPlanSchema(2028, direction),
				new EducationPlanSchema(2029, direction),
				new EducationPlanSchema(2030, direction),
			];
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest createRequest = new EducationPlanGeneralRequest(schema);
				new CreateEducationPlanTest(createRequest).DoOperation().Wait();
			}
			new GetPagedEducationPlanTest(1, 5).DoOperation().Wait();
			Console.WriteLine();
			new GetPagedEducationPlanTest(2, 5).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest deleteRequest = new EducationPlanGeneralRequest(schema);
				new DeleteEducationPlanTest(deleteRequest).DoOperation().Wait();
			}
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}

		public void GetFilteredTest()
		{
			EducationDirectionSchema direction1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			EducationDirectionSchema direction2 = new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат");
			EducationDirectionSchema direction3 = new EducationDirectionSchema("01.01.01", "Физика", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest1 = new CreateEducationDirectionRequest(direction1);
			CreateEducationDirectionRequest createDirectionRequest2 = new CreateEducationDirectionRequest(direction2);
			CreateEducationDirectionRequest createDirectionRequest3 = new CreateEducationDirectionRequest(direction3);
			new CreateEducationDirectionTest(createDirectionRequest1).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest2).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest3).DoOperation().Wait();
			EducationPlanSchema[] schemas = [
				new EducationPlanSchema(2021, direction1),
				new EducationPlanSchema(2022, direction1),
				new EducationPlanSchema(2023, direction1),
				new EducationPlanSchema(2024, direction2),
				new EducationPlanSchema(2025, direction2),
				new EducationPlanSchema(2026, direction2),
				new EducationPlanSchema(2027, direction3),
				new EducationPlanSchema(2028, direction3),
				new EducationPlanSchema(2029, direction3),
				new EducationPlanSchema(2030, direction3),
			];
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest createRequest = new EducationPlanGeneralRequest(schema);
				new CreateEducationPlanTest(createRequest).DoOperation().Wait();
			}
			Console.WriteLine();
			EducationDirectionSchema filterDirectionSchema = new EducationDirectionSchema("09", "", "");
			EducationPlanSchema filterPlanSchema = new EducationPlanSchema(2024, filterDirectionSchema);
			EducationPlanGeneralRequest filterRequest = new EducationPlanGeneralRequest(filterPlanSchema);
			new GetFilteredEducationPlansTest(filterRequest).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest deleteRequest = new EducationPlanGeneralRequest(schema);
				new DeleteEducationPlanTest(deleteRequest).DoOperation().Wait();
			}
			DeleteEducationDirectionRequest deleteDirectionRequest1 = new DeleteEducationDirectionRequest(direction1);
			DeleteEducationDirectionRequest deleteDirectionRequest2 = new DeleteEducationDirectionRequest(direction2);
			DeleteEducationDirectionRequest deleteDirectionRequest3 = new DeleteEducationDirectionRequest(direction3);
			new DeleteEducationDirectionTest(deleteDirectionRequest1).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest2).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest3).DoOperation().Wait();
		}

		public void GetFilteredByYearTest()
		{
			EducationDirectionSchema direction1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			EducationDirectionSchema direction2 = new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат");
			EducationDirectionSchema direction3 = new EducationDirectionSchema("01.01.01", "Физика", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest1 = new CreateEducationDirectionRequest(direction1);
			CreateEducationDirectionRequest createDirectionRequest2 = new CreateEducationDirectionRequest(direction2);
			CreateEducationDirectionRequest createDirectionRequest3 = new CreateEducationDirectionRequest(direction3);
			new CreateEducationDirectionTest(createDirectionRequest1).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest2).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest3).DoOperation().Wait();
			EducationPlanSchema[] schemas = [
				new EducationPlanSchema(2021, direction1),
				new EducationPlanSchema(2022, direction1),
				new EducationPlanSchema(2023, direction1),
				new EducationPlanSchema(2024, direction2),
				new EducationPlanSchema(2025, direction2),
				new EducationPlanSchema(2026, direction2),
				new EducationPlanSchema(2027, direction3),
				new EducationPlanSchema(2028, direction3),
				new EducationPlanSchema(2029, direction3),
				new EducationPlanSchema(2030, direction3),
			];
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest createRequest = new EducationPlanGeneralRequest(schema);
				new CreateEducationPlanTest(createRequest).DoOperation().Wait();
			}
			Console.WriteLine();
			EducationDirectionSchema filterDirectionSchema = new EducationDirectionSchema("", "", "");
			EducationPlanSchema filterPlanSchema = new EducationPlanSchema(2024, filterDirectionSchema);
			EducationPlanGeneralRequest filterRequest = new EducationPlanGeneralRequest(filterPlanSchema);
			new GetFilteredEducationPlansByYear(filterRequest).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest deleteRequest = new EducationPlanGeneralRequest(schema);
				new DeleteEducationPlanTest(deleteRequest).DoOperation().Wait();
			}
			DeleteEducationDirectionRequest deleteDirectionRequest1 = new DeleteEducationDirectionRequest(direction1);
			DeleteEducationDirectionRequest deleteDirectionRequest2 = new DeleteEducationDirectionRequest(direction2);
			DeleteEducationDirectionRequest deleteDirectionRequest3 = new DeleteEducationDirectionRequest(direction3);
			new DeleteEducationDirectionTest(deleteDirectionRequest1).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest2).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest3).DoOperation().Wait();
		}

		public void GetFilteredByDirection()
		{
			EducationDirectionSchema direction1 = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			EducationDirectionSchema direction2 = new EducationDirectionSchema("09.03.04", "Программная инженерия", "Бакалавриат");
			EducationDirectionSchema direction3 = new EducationDirectionSchema("01.01.01", "Физика", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest1 = new CreateEducationDirectionRequest(direction1);
			CreateEducationDirectionRequest createDirectionRequest2 = new CreateEducationDirectionRequest(direction2);
			CreateEducationDirectionRequest createDirectionRequest3 = new CreateEducationDirectionRequest(direction3);
			new CreateEducationDirectionTest(createDirectionRequest1).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest2).DoOperation().Wait();
			new CreateEducationDirectionTest(createDirectionRequest3).DoOperation().Wait();
			EducationPlanSchema[] schemas = [
				new EducationPlanSchema(2021, direction1),
				new EducationPlanSchema(2022, direction1),
				new EducationPlanSchema(2023, direction1),
				new EducationPlanSchema(2024, direction2),
				new EducationPlanSchema(2025, direction2),
				new EducationPlanSchema(2026, direction2),
				new EducationPlanSchema(2027, direction3),
				new EducationPlanSchema(2028, direction3),
				new EducationPlanSchema(2029, direction3),
				new EducationPlanSchema(2030, direction3),
			];
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest createRequest = new EducationPlanGeneralRequest(schema);
				new CreateEducationPlanTest(createRequest).DoOperation().Wait();
			}
			Console.WriteLine();
			EducationDirectionSchema filterDirectionSchema = new EducationDirectionSchema("09", "", "");
			EducationPlanSchema filterPlanSchema = new EducationPlanSchema(2024, filterDirectionSchema);
			EducationPlanGeneralRequest filterRequest = new EducationPlanGeneralRequest(filterPlanSchema);
			new GetFilteredEducationPlansByDirection(filterRequest).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				EducationPlanGeneralRequest deleteRequest = new EducationPlanGeneralRequest(schema);
				new DeleteEducationPlanTest(deleteRequest).DoOperation().Wait();
			}
			DeleteEducationDirectionRequest deleteDirectionRequest1 = new DeleteEducationDirectionRequest(direction1);
			DeleteEducationDirectionRequest deleteDirectionRequest2 = new DeleteEducationDirectionRequest(direction2);
			DeleteEducationDirectionRequest deleteDirectionRequest3 = new DeleteEducationDirectionRequest(direction3);
			new DeleteEducationDirectionTest(deleteDirectionRequest1).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest2).DoOperation().Wait();
			new DeleteEducationDirectionTest(deleteDirectionRequest3).DoOperation().Wait();
		}
	}

	public class FailureScenarios
	{
		public static FailureScenarios BuildFailure() => new FailureScenarios();

		public void CreateWithoutDirection()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			EducationPlanSchema planSchema = new EducationPlanSchema(2021, direction);
			EducationPlanGeneralRequest request = new EducationPlanGeneralRequest(planSchema);
			new CreateEducationPlanTest(request).DoOperation().Wait();
		}

		public void CreateWithDublication()
		{
			EducationDirectionSchema direction = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createDirectionRequest = new CreateEducationDirectionRequest(direction);
			new CreateEducationDirectionTest(createDirectionRequest).DoOperation().Wait();
			EducationPlanSchema planSchema = new EducationPlanSchema(2021, direction);
			EducationPlanGeneralRequest request = new EducationPlanGeneralRequest(planSchema);
			new CreateEducationPlanTest(request).DoOperation().Wait();
			new CreateEducationPlanTest(request).DoOperation().Wait();
			new DeleteEducationPlanTest(request).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteDirectionRequest = new DeleteEducationDirectionRequest(direction);
			new DeleteEducationDirectionTest(deleteDirectionRequest).DoOperation().Wait();
		}
	}
}
