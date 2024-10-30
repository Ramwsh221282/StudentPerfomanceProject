namespace SPerfomance.Domain.Tools;

public static class ByteExtensions
{
	public static byte ValueOrEmpty(this byte? value)
	{
		return value == null ? default : value.Value;
	}
}
