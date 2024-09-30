using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Commands.Create;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;

internal sealed class CreateDirectionTest(EducationDirectionSchema schema) : IService<EducationDirection>
{
	private readonly EducationDirectionSchema _schema = schema;
	public async Task<OperationResult<EducationDirection>> DoOperation()
	{
		EducationDirectionsRepositoryObject parameter = EducationDirectionSchemaConverter.ToRepositoryObject(_schema);
		IRepository<EducationDirection> _repository = RepositoryProvider.CreateDirectionsRepository();
		IRepositoryExpression<EducationDirection> _checkForCodeDublicate = EducationDirectionExpressionsFactory.FindDirectionByCode(parameter); ;
		IService<EducationDirection> service = new EducationDirectionCreationService
		(
			_schema,
			_checkForCodeDublicate,
			_repository
		);
		OperationResult<EducationDirection> result = await service.DoOperation();
		OperationResultLogger<OperationResult<EducationDirection>, EducationDirection> logger =
		new(result, "Education Direction Creation Log");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			Console.WriteLine("\n");
			Console.WriteLine("Created Education Direction:");
			Console.WriteLine($"Direction ID: {result.Result.Id}");
			Console.WriteLine($"Direction Entity Number: {result.Result.EntityNumber}");
			Console.WriteLine($"Direction Name: {result.Result.Name}");
			Console.WriteLine($"Direction Type: {result.Result.Type}");
			Console.WriteLine($"Direction Code: {result.Result.Code}");
			Console.WriteLine("\n");
		}
		return result;
	}
}
