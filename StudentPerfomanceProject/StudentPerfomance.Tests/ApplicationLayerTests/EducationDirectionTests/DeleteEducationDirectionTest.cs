using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationDirections.Delete;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class DeleteEducationDirectionTest(DeleteEducationDirectionRequest request) : IService<EducationDirection>
{
	private readonly EducationDirectionSchema _direction = request.Direction;
	private IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<EducationDirection>> DoOperation()
	{
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_direction);
		IService<EducationDirection> service = new DeleteEducationDirectionService
		(
			EducationDirectionExpressionsFactory.FindDirection(parameter),
			_repository
		);
		OperationResult<EducationDirection> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationDirection>, EducationDirection> logger =
		new(result, "Education Direction Deletion Test");
		logger.ShowInfo();
		if (result.Result != null)
		{
			Console.WriteLine("Education direction deletion info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Code: {result.Result.Code.Code}");
			Console.WriteLine($"Name: {result.Result.Name.Name}");
			Console.WriteLine($"Type: {result.Result.Type.Type}");
		}
		return result;
	}
}
