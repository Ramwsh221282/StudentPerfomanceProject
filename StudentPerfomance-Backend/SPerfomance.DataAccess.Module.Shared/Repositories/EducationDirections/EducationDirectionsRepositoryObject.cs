namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;

public sealed class EducationDirectionsRepositoryObject
{
	public string Code { get; private set; } = string.Empty;
	public string Name { get; private set; } = string.Empty;
	public string Type { get; private set; } = string.Empty;

	public EducationDirectionsRepositoryObject WithDirectionCode(string code)
	{
		if (!string.IsNullOrWhiteSpace(code)) Code = code;
		return this;
	}

	public EducationDirectionsRepositoryObject WithDirectionName(string name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}

	public EducationDirectionsRepositoryObject WithDirectionType(string type)
	{
		if (!string.IsNullOrWhiteSpace(type)) Type = type;
		return this;
	}
}
