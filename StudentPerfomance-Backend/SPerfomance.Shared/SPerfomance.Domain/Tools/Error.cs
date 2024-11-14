namespace SPerfomance.Domain.Tools;

public sealed record Error(string? Description = null)
{
    public static readonly Error None = new Error(string.Empty);
}
