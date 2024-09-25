using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.Queries.EducationDirections.CollectionPagedFilters;
using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Tests.ApplicationLayerTests.EducationDirectionTests;

public sealed class FilterPagedByNameEducationDirectionTest(FilterEducationDirectionRequest request, int page, int pageSize) : IService<IReadOnlyCollection<EducationDirection>>
{
	private readonly EducationDirectionSchema schema = request.Direction;
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> DoOperation()
	{
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(schema);
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterByNameExpression(parameter);
		IService<IReadOnlyCollection<EducationDirection>> service = EducationDirectionGetPagedCollectionByFilterBuilder.Build
		(
			new FilterConstraint(FilterConstraint.NameOnly),
			_page,
			_pageSize,
			_repository,
			expression
		);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await service.DoOperation();
		OperationResultLogger<OperationResult<IReadOnlyCollection<EducationDirection>>, IReadOnlyCollection<EducationDirection>> logger =
		new(result, "Education Direction Paged Filter By Name Test");
		logger.ShowInfo();
		if (result.Result != null && result.Result.Count > 0)
		{
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
