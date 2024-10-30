using SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.EducationDirections.Contracts;

public class EducationDirectionContract
{
	public string? Name { get; set; }

	public string? Code { get; set; }

	public string? Type { get; set; }

	public static implicit operator GetEducationDirectionQuery(EducationDirectionContract contract) =>
		new GetEducationDirectionQuery(contract.Name.ValueOrEmpty(), contract.Code.ValueOrEmpty(), contract.Type.ValueOrEmpty());

	public static implicit operator CreateEducationDirectionCommand(EducationDirectionContract contract) =>
		new CreateEducationDirectionCommand(contract.Name.ValueOrEmpty(), contract.Code.ValueOrEmpty(), contract.Type.ValueOrEmpty());
}
