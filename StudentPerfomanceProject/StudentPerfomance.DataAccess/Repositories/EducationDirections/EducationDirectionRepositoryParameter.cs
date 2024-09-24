namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public sealed class EducationDirectionRepositoryParameter
{
	public string? Code { get; private set; }
	public string? Name { get; private set; }
	public string? Type { get; private set; }

	public EducationDirectionRepositoryParameter WithDirectionCode(string? directionCode)
	{
		Code = directionCode;
		return this;
	}

	public EducationDirectionRepositoryParameter WithDirectionName(string? directionName)
	{
		Name = directionName;
		return this;
	}

	public EducationDirectionRepositoryParameter WithDirectionType(string? directionType)
	{
		Type = directionType;
		return this;
	}
}
