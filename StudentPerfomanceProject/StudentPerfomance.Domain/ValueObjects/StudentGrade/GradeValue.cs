using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.GradeValue;

namespace StudentPerfomance.Domain.ValueObjects.StudentGrade;

public class GradeValue : ValueObject
{
    private GradeValue() { }

    public static GradeValue NotAttestated = new GradeValue("н/а");
    public static GradeValue Bad = new GradeValue("2");
    public static GradeValue Satisfine = new GradeValue("3");
    public static GradeValue Good = new GradeValue("4");
    public static GradeValue VeryGood = new GradeValue("5");

    private GradeValue(string value) => Value = value;    

    public string Value { get; }

    public static Result<GradeValue> Create(string value)
    {
        Validator<GradeValue> validator = new GradeValueValidator(value);
        if (!validator.Validate())
            return Result.Failure<GradeValue>(validator.GetErrorText());
        return new GradeValue(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
