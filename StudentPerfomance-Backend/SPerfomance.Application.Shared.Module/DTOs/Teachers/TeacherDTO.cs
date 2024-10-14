using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.DTOs.Departments;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;

namespace SPerfomance.Application.Shared.Module.DTOs.Teachers;

public class TeacherDTO
{
	[JsonPropertyName("department")]
	public DepartmentDTO? Department { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("surname")]
	public string? Surname { get; set; }

	[JsonPropertyName("thirdname")]
	public string? Thirdname { get; set; }

	[JsonPropertyName("workingCondition")]
	public string? workingCondition { get; set; }

	[JsonPropertyName("jobTitle")]
	public string? jobTitle { get; set; }
}

public static class TeacherDTOExtensions
{
	public static TeacherSchema ToSchema(this TeacherDTO? dto)
	{
		if (dto == null)
			return new TeacherSchema();

		DepartmentSchema department = dto.Department.ToSchema();
		string name = dto.Name.CreateValueOrEmpty();
		string surname = dto.Surname.CreateValueOrEmpty();
		string thirdname = dto.Thirdname.CreateValueOrEmpty();
		string workingCondition = dto.workingCondition.CreateValueOrEmpty();
		string jobTitle = dto.jobTitle.CreateValueOrEmpty();

		return new TeacherSchema(name, surname, thirdname, workingCondition, jobTitle, department);
	}
}
