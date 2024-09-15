namespace StudentPerfomance.Domain.Validators.GradeValue;

internal class GradeValueValidator : Validator<ValueObjects.StudentGrade.GradeValue>
{    
    private readonly ValueObjects.StudentGrade.GradeValue[] _values = 
    {
        ValueObjects.StudentGrade.GradeValue.NotAttestated,
        ValueObjects.StudentGrade.GradeValue.Bad,
        ValueObjects.StudentGrade.GradeValue.Satisfine,
        ValueObjects.StudentGrade.GradeValue.Good,
        ValueObjects.StudentGrade.GradeValue.VeryGood,
    };

    private readonly string _value;
    public GradeValueValidator(string value) => _value = value;

    public override bool Validate()
    {
        if (_values.Any(item => item.Value == _value) == false)
        {
            _errorBuilder.AppendLine("Такой оценки не может существовать");
            return false;
        }
        return true;
    }
}
