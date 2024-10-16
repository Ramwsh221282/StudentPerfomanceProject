using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationDirections.Module.Api.Requests;
using SPerfomance.Application.EducationDirections.Module.Commands.CreateDirection;
using SPerfomance.Application.EducationDirections.Module.Commands.DeleteDirection;
using SPerfomance.Application.EducationDirections.Module.Commands.UpdateDirection;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api;

[ApiController]
[Route("/education-directions/api/management")]
public sealed class EducationDirectionsManagementApi : Controller
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<EducationDirectionSchema>> Create([FromBody] EducationDirectionSchema direction)
	{
		CreateEducationDirectionCommand command = new CreateEducationDirectionCommand(direction);
		OperationResult<EducationDirection> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<EducationDirectionSchema>> Delete([FromBody] EducationDirectionSchema direction)
	{
		DeleteEducationDirectionCommand command = new DeleteEducationDirectionCommand(direction);
		OperationResult<EducationDirection> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<EducationDirectionSchema>> Update([FromBody] UpdateRequest request)
	{
		UpdateDirectionCommand command = new UpdateDirectionCommand(request.initial, request.updated);
		OperationResult<EducationDirection> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
