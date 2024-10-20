using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api.Requests;

public record EducationDirectionSearchDataRequest(EducationDirectionDTO Direction, string Token);
