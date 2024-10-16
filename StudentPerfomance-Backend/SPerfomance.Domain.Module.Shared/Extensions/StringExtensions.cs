using System.Text;
using System.Text.RegularExpressions;

namespace SPerfomance.Domain.Module.Shared.Extensions;

public static class StringExtensions
{
	public static string CreateAcronymus(this string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return string.Empty;

		StringBuilder builder = new StringBuilder(input.FormatSpaces());
		string acronymus = builder.Replace(" - ", " ").Replace(" и ", " ").ToString().FormatSpaces();
		ReadOnlySpan<string> words = acronymus.Split(' ').AsSpan();
		builder = new StringBuilder();

		for (int index = 0; index < words.Length; index++)
		{
			builder.Append(words[index][0]);
		}

		acronymus = builder.ToString().ToUpper();
		return acronymus;
	}

	public static string FormatSpaces(this string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return string.Empty;

		string formatted = Regex.Replace(input, @"\s+", " ");
		return formatted;
	}

	public static string FirstCharacterToUpper(this string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return string.Empty;

		Span<char> dest = new char[1];
		input.AsSpan(0, 1).ToUpperInvariant(dest);

		return $"{dest}{input.AsSpan(1)}";
	}
}
