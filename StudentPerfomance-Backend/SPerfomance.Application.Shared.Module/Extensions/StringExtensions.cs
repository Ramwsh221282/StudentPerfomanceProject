namespace SPerfomance.Application.Shared.Module.Extensions;

public static class StringExtensions
{
	public static string CreateValueOrEmpty(this string? value) =>
		value == null || string.IsNullOrWhiteSpace(value) ? string.Empty : value;
}
