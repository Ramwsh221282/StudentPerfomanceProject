namespace SPerfomance.Domain.Tools;

public static class IntegerExtensions
{
	public static int ValueOrEmpty(this int? value) => value == null ? 0 : value.Value;

	public static ulong ValueOrEmpty(this ulong? value) => value == null ? 0 : value.Value;
}
