using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api.Requests;

public record EducationDirectionActionRequest(EducationDirectionDTO Direction, string Token);
