using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Commands.Update;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;

internal sealed class UpdateDirectionTest(EducationDirectionSchema oldDirection, EducationDirectionSchema newDirection)
: IService<EducationDirection>
{
	private readonly EducationDirectionSchema _oldDirection = oldDirection;
	private readonly EducationDirectionSchema _newDirection = newDirection;
	public async Task<OperationResult<EducationDirection>> DoOperation()
	{
		EducationDirectionsRepositoryObject oldDirection = EducationDirectionSchemaConverter.ToRepositoryObject(_oldDirection);
		EducationDirectionsRepositoryObject newDirection = EducationDirectionSchemaConverter.ToRepositoryObject(_newDirection);
		IService<EducationDirection> service = new EducationDirectionUpdateService
		(
			_newDirection,
			EducationDirectionExpressionsFactory.FindDirection(oldDirection),
			EducationDirectionExpressionsFactory.FindDirectionByCode(newDirection),
			RepositoryProvider.CreateDirectionsRepository()
		);
		var result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationDirection>, EducationDirection> logger =
		new(result, "Education Direction Update Log");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("\n");
			Console.WriteLine("Update Education Direction:");
			Console.WriteLine($"Direction ID: {result.Result.Id}");
			Console.WriteLine($"Direction Entity Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Direction New Name: {result.Result.Name} - Old Direction Name: {_oldDirection.Name}");
			Console.WriteLine($"Direction New Type: {result.Result.Type}");
			Console.WriteLine($"Direction New Code: {result.Result.Code} - Old Direction Code: {_oldDirection.Code}");
			Console.WriteLine("\n");
		}
		return result;
	}
}
