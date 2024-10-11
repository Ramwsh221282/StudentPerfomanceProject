using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;

namespace SPerfomance.Application.Shared.Module.DTOs.EducationDirections;

public class EducationDirectionDTO
{
	[JsonPropertyName("code")]
	public string? Code { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("type")]
	public string? Type { get; set; }
}

public static class EducationDirectionDTOExtensions
{
	public static EducationDirectionSchema ToSchema(this EducationDirectionDTO? dto)
	{
		if (dto == null)
			return new EducationDirectionSchema(string.Empty, string.Empty, string.Empty);

		string code = dto.Code.CreateValueOrEmpty();
		string name = dto.Name.CreateValueOrEmpty();
		string type = dto.Type.CreateValueOrEmpty();
		return new EducationDirectionSchema(code, name, type);
	}
}
