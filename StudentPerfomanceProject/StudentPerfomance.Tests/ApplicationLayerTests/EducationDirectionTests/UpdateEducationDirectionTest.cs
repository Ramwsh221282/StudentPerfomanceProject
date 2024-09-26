using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class UpdateEducationDirectionTest(UpdateEducationDirectionRequest request) : IService<EducationDirection>
{
	private readonly EducationDirectionSchema _oldSchema = request.OldSchema;
	private readonly EducationDirectionSchema _newSchema = request.NewSchema;
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<EducationDirection>> DoOperation()
	{
		EducationDirectionRepositoryParameter getOldSchema = EducationDirectionSchemaConverter.ToRepositoryParameter(_oldSchema);
		EducationDirectionRepositoryParameter dublicateSchema = EducationDirectionSchemaConverter.ToRepositoryParameter(_newSchema);
		IService<EducationDirection> service = new UpdateEducationDirectionService
		(
			_oldSchema,
			_newSchema,
			EducationDirectionExpressionsFactory.FindDirection(getOldSchema),
			EducationDirectionExpressionsFactory.FindDirectionByCode(dublicateSchema),
			_repository
		);
		OperationResult<EducationDirection> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationDirection>, EducationDirection> logger =
		new(result, "Education Direction Update Test Log");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("Education Direction update info:");
			Console.WriteLine($"ID: {result.Result.Id}");
			Console.WriteLine($"Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Code: {result.Result.Code.Code}");
			Console.WriteLine($"Name: {result.Result.Name.Name}");
			Console.WriteLine($"Type: {result.Result.Type.Type}");
		}
		return result;
	}
}
