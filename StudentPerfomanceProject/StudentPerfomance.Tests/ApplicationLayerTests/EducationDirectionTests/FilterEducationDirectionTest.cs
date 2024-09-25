using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.Queries.EducationDirections.CollectionFilters;
using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class FilterEducationDirectionTest(FilterEducationDirectionRequest request) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly EducationDirectionSchema _direction = request.Direction;
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_direction);
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterExpression(parameter);
		IService<IReadOnlyCollection<EducationDirection>> service = EducationDirectionGetCollectionByFilterBuilder.Build(new FilterConstraint(FilterConstraint.General), _repository, expression);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationDirection>>, IReadOnlyCollection<EducationDirection>> logger =
		new(result, "Education Direction General Filter Request Test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
			Console.WriteLine("Fetch all education directions info:");
			foreach (var direction in result.Result)
			{
				Console.WriteLine($"ID: {direction.Id}");
				Console.WriteLine($"Number: {direction.EntityNumber}");
				Console.WriteLine($"Code: {direction.Code.Code}");
				Console.WriteLine($"Name: {direction.Name.Name}");
				Console.WriteLine($"Type: {direction.Type.Type}");
				Console.WriteLine();
			}
		}
		return result;
	}
}
