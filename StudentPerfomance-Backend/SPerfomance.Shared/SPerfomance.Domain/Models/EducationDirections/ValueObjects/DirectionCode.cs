using System.Text.RegularExpressions;

using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.ValueObjects;

public class DirectionCode : DomainValueObject
{
	private static Regex _pattern = new Regex(@"^\d+\.\d+\.\d+$");

	private const int codeMaxLength = 15;

	private DirectionCode() => Code = string.Empty;

	private DirectionCode(string code) => Code = code;

	public string Code { get; private set; }

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Code;
	}

	public static DirectionCode Empty => new DirectionCode();

	public static Result<DirectionCode> Create(string code)
	{
		if (string.IsNullOrWhiteSpace(code))
			return Result<DirectionCode>.Failure(EducationDirectionErrors.CodeEmptyError());

		if (code.Length > codeMaxLength)
			return Result<DirectionCode>.Failure(EducationDirectionErrors.CodeExceessLengthError(codeMaxLength));

		if (!_pattern.Match(code).Success)
			return Result<DirectionCode>.Failure(EducationDirectionErrors.CodeInvalidError(code));

		return Result<DirectionCode>.Success(new DirectionCode(code));
	}
}
