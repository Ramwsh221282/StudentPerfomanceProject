using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

public class JobTitle : ValueObject
{
	public string Value { get; init; }
	protected JobTitle() { Value = string.Empty; }
	protected JobTitle(string value) { Value = value; }
	public static JobTitle CreateDefault() => new JobTitle();
	public static CSharpFunctionalExtensions.Result<JobTitle> Create(string value)
	{
		JobTitle jobTitle = new JobTitle(value);
		Validator<JobTitle> validator = new JobTitleValidator(jobTitle);
		return validator.Validate() ? jobTitle : CSharpFunctionalExtensions.Result.Failure<JobTitle>(validator.GetErrorText());
	}
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
	public override string ToString() => Value;
}
