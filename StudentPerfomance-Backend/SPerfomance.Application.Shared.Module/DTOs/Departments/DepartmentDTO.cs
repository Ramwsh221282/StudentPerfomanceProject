using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.Departments;

namespace SPerfomance.Application.Shared.Module.DTOs.Departments;

public class DepartmentDTO
{
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("shortName")]
	public string? Shortname { get; set; }
}

public static class DepartmentDTOExtensions
{
	public static DepartmentSchema ToSchema(this DepartmentDTO? dto)
	{
		if (dto == null)
			return new DepartmentSchema();

		string name = dto.Name.CreateValueOrEmpty();
		string shortname = dto.Shortname.CreateValueOrEmpty();
		return new DepartmentSchema(name, shortname);
	}
}
