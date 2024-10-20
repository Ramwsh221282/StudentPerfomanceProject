using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api.Requests;

public record EducationDirectionFilterDataRequest(int Page, int PageSize, string Token, EducationDirectionDTO Direction);
