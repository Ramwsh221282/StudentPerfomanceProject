using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.Students;

namespace SPerfomance.Application.Shared.Module.DTOs.Students;

public class StudentDTO
{
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("surname")]
	public string? Surname { get; set; }

	[JsonPropertyName("thirdname")]
	public string? Thirdname { get; set; }

	[JsonPropertyName("state")]
	public string? State { get; set; }

	[JsonPropertyName("recordbook")]
	public ulong Recordbook { get; set; }

	[JsonPropertyName("group")]
	public StudentGroupDTO? Group { get; set; }
}

public static class StudentDTOExtensions
{
	public static StudentSchema ToSchema(this StudentDTO? dto)
	{
		if (dto == null)
			return new StudentSchema();

		string name = dto.Name.CreateValueOrEmpty();
		string surname = dto.Surname.CreateValueOrEmpty();
		string thirdname = dto.Thirdname.CreateValueOrEmpty();
		string state = dto.State.CreateValueOrEmpty();
		ulong recordBook = dto.Recordbook;
		StudentsGroupSchema group = dto.Group.ToSchema();
		return new StudentSchema(name, surname, thirdname, state, recordBook, group);
	}
}
