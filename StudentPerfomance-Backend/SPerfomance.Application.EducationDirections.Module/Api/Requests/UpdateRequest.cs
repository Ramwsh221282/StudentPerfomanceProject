using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api.Requests;

public record UpdateRequest(EducationDirectionSchema initial, EducationDirectionSchema updated);
