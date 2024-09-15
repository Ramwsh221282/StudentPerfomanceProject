namespace StudentPerfomance.DataAccess.Repositories;

public static class RepositoryStringExtensions
{
	public static bool NullableContains(this string? entryString, string? targetString)
	{
		if (string.IsNullOrWhiteSpace(entryString) || string.IsNullOrWhiteSpace(targetString))
			return false;
		return entryString.Contains(targetString);
	}

	public static bool NullableStartsWith(this string? entryString, string? targetString)
	{
		if (string.IsNullOrWhiteSpace(entryString) || string.IsNullOrWhiteSpace(targetString))
			return false;
		return entryString.StartsWith(targetString);
	}
}
