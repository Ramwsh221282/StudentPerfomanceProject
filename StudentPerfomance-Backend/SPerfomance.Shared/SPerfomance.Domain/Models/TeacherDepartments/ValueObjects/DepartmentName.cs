using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;

public class DepartmentName : DomainValueObject
{
    public const int MaxNameLength = 150;

    public const int MinNameLength = 10;

    public string Name { get; private set; }

    private DepartmentName(string name) => Name = name;

    private DepartmentName() => Name = string.Empty;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public static DepartmentName Empty => new DepartmentName();

    public static Result<DepartmentName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<DepartmentName>.Failure(
                TeacherDepartmentErrors.DepartmentNameEmptyError()
            );

        return name.Length switch
        {
            > MaxNameLength => Result<DepartmentName>.Failure(
                TeacherDepartmentErrors.DepartmentNameExceessError(MaxNameLength)
            ),
            < MinNameLength => Result<DepartmentName>.Failure(
                TeacherDepartmentErrors.DepartmentNameLessError(MinNameLength)
            ),
            _ => Result<DepartmentName>.Success(new DepartmentName(name)),
        };
    }
}
