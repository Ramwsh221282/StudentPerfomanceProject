using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api.Requests;

public record UpdateRequest(EducationDirectionDTO Initial, EducationDirectionDTO Updated, string Token);
