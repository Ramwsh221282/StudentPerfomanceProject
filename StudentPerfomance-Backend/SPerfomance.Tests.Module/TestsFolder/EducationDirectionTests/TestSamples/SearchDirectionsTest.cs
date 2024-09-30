using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Queries.GetFiltered;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Tests.Module.TestsFolder.EducationDirectionTests.TestSamples;
internal sealed class SearchDirectionsTest(EducationDirectionSchema schema) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly EducationDirectionSchema _schema = schema;
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		EducationDirectionsRepositoryObject parameter = EducationDirectionSchemaConverter.ToRepositoryObject(_schema);
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetByFilter
		(
			RepositoryProvider.CreateDirectionsRepository(),
			EducationDirectionExpressionsFactory.FilterExpression(parameter)
		);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationDirection>>, IReadOnlyCollection<EducationDirection>> logger
		= new(result, "Education Directions Search Log Info");
		logger.ShowInfo();
		if (result.Result != null && !result.IsFailed)
		{
			foreach (var direction in result.Result)
			{
				Console.WriteLine("\n");
				Console.WriteLine($"Direction ID: {direction.Id}");
				Console.WriteLine($"Direction Entity Number: {direction.EntityNumber}");
				Console.WriteLine($"Direction Name: {direction.Name}");
				Console.WriteLine($"Direction Type: {direction.Type}");
				Console.WriteLine($"Direction Code: {direction.Code}");
				Console.WriteLine("\n");
			}
		}
		return result;
	}
}
