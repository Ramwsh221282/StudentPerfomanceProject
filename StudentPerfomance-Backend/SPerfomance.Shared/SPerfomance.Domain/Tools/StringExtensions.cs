using System.Text;
using System.Text.RegularExpressions;

namespace SPerfomance.Domain.Tools;

public static class StringExtensions
{
    public static string ValueOrEmpty(this string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
    }

    public static string FormatSpaces(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        string formatted = Regex.Replace(input, @"\s+", " ");
        return formatted;
    }

    public static string CreateAcronymus(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        StringBuilder builder = new StringBuilder(input.FormatSpaces());
        string acronymus = builder
            .Replace("-", " ")
            .Replace(" - ", " ")
            .Replace(" и ", " ")
            .ToString()
            .FormatSpaces();
        ReadOnlySpan<string> words = acronymus.Split(' ').AsSpan();
        builder = new StringBuilder();

        for (int index = 0; index < words.Length; index++)
        {
            builder.Append(words[index][0]);
        }

        acronymus = builder.ToString().ToUpper();
        return acronymus;
    }
}
