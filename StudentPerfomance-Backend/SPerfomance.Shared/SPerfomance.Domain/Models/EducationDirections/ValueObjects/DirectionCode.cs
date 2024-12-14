using System.Text.RegularExpressions;
using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.ValueObjects;

public class DirectionCode : DomainValueObject
{
    private static readonly Regex Pattern = new(@"^\d+\.\d+\.\d+$");

    private const int CodeMaxLength = 15;

    private DirectionCode() => Code = string.Empty;

    private DirectionCode(string code) => Code = code.Trim();

    public string Code { get; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public static DirectionCode Empty => new DirectionCode();

    public static Result<DirectionCode> Create(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result<DirectionCode>.Failure(EducationDirectionErrors.CodeEmptyError());

        if (code.Length > CodeMaxLength)
            return Result<DirectionCode>.Failure(
                EducationDirectionErrors.CodeExceessLengthError(CodeMaxLength)
            );

        return !Pattern.Match(code).Success
            ? Result<DirectionCode>.Failure(EducationDirectionErrors.CodeInvalidError(code))
            : Result<DirectionCode>.Success(new DirectionCode(code));
    }
}
