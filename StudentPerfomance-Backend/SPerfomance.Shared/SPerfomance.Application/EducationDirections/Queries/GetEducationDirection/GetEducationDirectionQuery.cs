using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;

public class GetEducationDirectionQuery(string name, string code, string type) : IQuery<EducationDirection>
{
	public string Name { get; init; } = name;

	public string Code { get; init; } = code;

	public string Type { get; init; } = type;
}
