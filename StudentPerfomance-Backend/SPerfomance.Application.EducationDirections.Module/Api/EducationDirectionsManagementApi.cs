using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationDirections.Module.Api.Requests;
using SPerfomance.Application.EducationDirections.Module.Commands.CreateDirection;
using SPerfomance.Application.EducationDirections.Module.Commands.DeleteDirection;
using SPerfomance.Application.EducationDirections.Module.Commands.UpdateDirection;
using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api;

[ApiController]
[Route("/education-directions/api/management")]
public sealed class EducationDirectionsManagementApi : Controller
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<EducationDirectionSchema>> Create([FromBody] EducationDirectionActionRequest request)
	{
		EducationDirectionDTO direction = request.Direction;
		string token = request.Token;
		CreateEducationDirectionCommand command = new CreateEducationDirectionCommand(direction, token);
		OperationResult<EducationDirection> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<EducationDirectionSchema>> Delete([FromBody] EducationDirectionActionRequest request)
	{
		EducationDirectionDTO direction = request.Direction;
		string token = request.Token;
		DeleteEducationDirectionCommand command = new DeleteEducationDirectionCommand(direction, token);
		OperationResult<EducationDirection> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<EducationDirectionSchema>> Update([FromBody] UpdateRequest request)
	{
		EducationDirectionDTO initial = request.Initial;
		EducationDirectionDTO updated = request.Updated;
		string token = request.Token;
		UpdateDirectionCommand command = new UpdateDirectionCommand(initial, updated, token);
		OperationResult<EducationDirection> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
