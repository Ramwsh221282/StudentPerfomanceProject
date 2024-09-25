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

		public void UpdateNameSuccess()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();

			EducationDirectionSchema newSchema = new EducationDirectionSchema("09.03.01", "Программная инженерия", "Бакалавриат");
			UpdateEducationDirectionNameRequest updateRequest = new UpdateEducationDirectionNameRequest(schema, newSchema);
			new UpdateEducationDirectionNameTest(updateRequest).DoOperation().Wait();

			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();

			DeleteEducationDirectionRequest deleteNewRequest = new DeleteEducationDirectionRequest(newSchema);
			new DeleteEducationDirectionTest(deleteNewRequest).DoOperation().Wait();
		}

		public void FetchAllTest()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("09.03.03", "E3", "Бакалавриат"),
				new("09.03.04", "E4", "Бакалавриат"),
				new("09.03.05", "E5", "Бакалавриат"),
				new("09.03.06", "E6", "Бакалавриат"),
				new("09.03.07", "E7", "Бакалавриат"),
				new("09.03.08", "E8", "Бакалавриат"),
				new("09.03.09", "E9", "Бакалавриат"),
				new("09.03.10", "E10", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			new GetAllEducationDirectionsTest().DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void FetchPaged()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("09.03.03", "E3", "Бакалавриат"),
				new("09.03.04", "E4", "Бакалавриат"),
				new("09.03.05", "E5", "Бакалавриат"),
				new("09.03.06", "A6", "Бакалавриат"),
				new("09.03.07", "A7", "Бакалавриат"),
				new("09.03.08", "A8", "Бакалавриат"),
				new("09.03.09", "A9", "Бакалавриат"),
				new("09.03.10", "A10", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			new GetPagedDirectionsTest(2, 5).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void FetchFiltered()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("09.03.03", "E3", "Бакалавриат"),
				new("09.03.04", "E4", "Бакалавриат"),
				new("09.03.05", "E5", "Бакалавриат"),
				new("09.03.06", "A1", "Бакалавриат"),
				new("09.03.07", "A2", "Бакалавриат"),
				new("09.03.08", "A3", "Бакалавриат"),
				new("09.03.09", "A4", "Бакалавриат"),
				new("09.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			EducationDirectionSchema filterSchema = new EducationDirectionSchema("", "A", "");
			FilterEducationDirectionRequest filterRequest = new FilterEducationDirectionRequest(filterSchema);
			new FilterEducationDirectionTest(filterRequest).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void FetchFilteredByName()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("09.03.03", "E3", "Бакалавриат"),
				new("09.03.04", "E4", "Бакалавриат"),
				new("09.03.05", "E5", "Бакалавриат"),
				new("09.03.06", "A1", "Бакалавриат"),
				new("09.03.07", "A2", "Бакалавриат"),
				new("09.03.08", "A3", "Бакалавриат"),
				new("09.03.09", "A4", "Бакалавриат"),
				new("09.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			EducationDirectionSchema filterSchema = new EducationDirectionSchema("", "E", "");
			FilterEducationDirectionRequest filterRequest = new FilterEducationDirectionRequest(filterSchema);
			new FilterEducationDirectionByNameTest(filterRequest).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void FetchFilteredByCode()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("03.03.03", "E3", "Бакалавриат"),
				new("03.03.04", "E4", "Бакалавриат"),
				new("04.03.05", "E5", "Бакалавриат"),
				new("05.03.06", "A1", "Бакалавриат"),
				new("06.03.07", "A2", "Бакалавриат"),
				new("03.03.08", "A3", "Бакалавриат"),
				new("06.03.09", "A4", "Бакалавриат"),
				new("08.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			EducationDirectionSchema filterSchema = new EducationDirectionSchema("08", "", "");
			FilterEducationDirectionRequest filterRequest = new FilterEducationDirectionRequest(filterSchema);
			new FilterEducationDirectionByCodeTest(filterRequest).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void PagedFetchFiltered()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.00", "Информатика и вычислительная техника", "Бакалавриат"),
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("09.03.03", "E3", "Бакалавриат"),
				new("09.03.04", "E4", "Бакалавриат"),
				new("09.03.05", "E5", "Бакалавриат"),
				new("09.03.06", "A1", "Бакалавриат"),
				new("09.03.07", "A2", "Бакалавриат"),
				new("09.03.08", "A3", "Бакалавриат"),
				new("09.03.09", "A4", "Бакалавриат"),
				new("09.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			EducationDirectionSchema filterSchema = new EducationDirectionSchema("", "Информатика", "");
			FilterEducationDirectionRequest filterRequest = new FilterEducationDirectionRequest(filterSchema);
			new FilterPagedEducationDirection(filterRequest, 1, 5).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void PagedFetchFilteredByName()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("09.03.03", "E3", "Бакалавриат"),
				new("09.03.04", "E4", "Бакалавриат"),
				new("09.03.05", "E5", "Бакалавриат"),
				new("09.03.06", "A1", "Бакалавриат"),
				new("09.03.07", "A2", "Бакалавриат"),
				new("09.03.08", "A3", "Бакалавриат"),
				new("09.03.09", "A4", "Бакалавриат"),
				new("09.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			EducationDirectionSchema filterSchema = new EducationDirectionSchema("", "E", "");
			FilterEducationDirectionRequest filterRequest = new FilterEducationDirectionRequest(filterSchema);
			new FilterPagedByNameEducationDirectionTest(filterRequest, 1, 5).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void PagedFetchFilteredByCode()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("03.03.03", "E3", "Бакалавриат"),
				new("03.03.04", "E4", "Бакалавриат"),
				new("04.03.05", "E5", "Бакалавриат"),
				new("05.03.06", "A1", "Бакалавриат"),
				new("06.03.07", "A2", "Бакалавриат"),
				new("03.03.08", "A3", "Бакалавриат"),
				new("06.03.09", "A4", "Бакалавриат"),
				new("08.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			EducationDirectionSchema filterSchema = new EducationDirectionSchema("08", "", "");
			FilterEducationDirectionRequest filterRequest = new FilterEducationDirectionRequest(filterSchema);
			new FilterPagedByCodeEducationDirectionTest(filterRequest, 1, 5).DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
		}

		public void CountTest()
		{
			EducationDirectionSchema[] schemas =
			[
				new("09.03.01", "E1", "Бакалавриат"),
				new("09.03.02", "E2", "Бакалавриат"),
				new("03.03.03", "E3", "Бакалавриат"),
				new("03.03.04", "E4", "Бакалавриат"),
				new("04.03.05", "E5", "Бакалавриат"),
				new("05.03.06", "A1", "Бакалавриат"),
				new("06.03.07", "A2", "Бакалавриат"),
				new("03.03.08", "A3", "Бакалавриат"),
				new("06.03.09", "A4", "Бакалавриат"),
				new("08.03.10", "A5", "Бакалавриат"),
			];
			foreach (var schema in schemas)
			{
				CreateEducationDirectionRequest request = new CreateEducationDirectionRequest(schema);
				new CreateEducationDirectionTest(request).DoOperation().Wait();
			}
			new EducationDirectionCountTest().DoOperation().Wait();
			foreach (var schema in schemas)
			{
				DeleteEducationDirectionRequest request = new DeleteEducationDirectionRequest(schema);
				new DeleteEducationDirectionTest(request).DoOperation().Wait();
			}
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

		public void CreateWithDublicateCode()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			EducationDirectionSchema newSchema = new EducationDirectionSchema("09.03.01", "Программная инженерия", "Бакалавриат");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();
			CreateEducationDirectionRequest createNewRequest = new CreateEducationDirectionRequest(newSchema);
			new CreateEducationDirectionTest(createNewRequest).DoOperation().Wait();
			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();
		}

		public void UpdateNameWithNotExisted()
		{
			EducationDirectionSchema schema = new EducationDirectionSchema("09.03.01", "Информатика и вычислительная техника", "Бакалавриат");
			CreateEducationDirectionRequest createRequest = new CreateEducationDirectionRequest(schema);
			new CreateEducationDirectionTest(createRequest).DoOperation().Wait();

			EducationDirectionSchema notExistedSchema = new EducationDirectionSchema("09.03.01", "Каокй-то текст", "Бакалавриат");
			EducationDirectionSchema newSchema = new EducationDirectionSchema("09.03.01", "Программная инженерия", "Бакалавриат");
			UpdateEducationDirectionNameRequest updateRequest = new UpdateEducationDirectionNameRequest(notExistedSchema, newSchema);
			new UpdateEducationDirectionNameTest(updateRequest).DoOperation().Wait();

			DeleteEducationDirectionRequest deleteRequest = new DeleteEducationDirectionRequest(schema);
			new DeleteEducationDirectionTest(deleteRequest).DoOperation().Wait();

			DeleteEducationDirectionRequest deleteNewRequest = new DeleteEducationDirectionRequest(newSchema);
			new DeleteEducationDirectionTest(deleteNewRequest).DoOperation().Wait();
		}
	}
}
