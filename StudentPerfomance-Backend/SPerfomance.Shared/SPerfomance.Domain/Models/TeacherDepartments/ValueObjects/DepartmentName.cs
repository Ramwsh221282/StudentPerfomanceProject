using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;

public class DepartmentName : DomainValueObject
{
    public const int MAX_NAME_LENGTH = 80;

    public const int MIN_NAME_LENGTH = 10;

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

        if (name.Length > MAX_NAME_LENGTH)
            return Result<DepartmentName>.Failure(
                TeacherDepartmentErrors.DepartmentNameExceessError(MAX_NAME_LENGTH)
            );

        if (name.Length < MIN_NAME_LENGTH)
            return Result<DepartmentName>.Failure(
                TeacherDepartmentErrors.DepartmentNameLessError(MIN_NAME_LENGTH)
            );

        return Result<DepartmentName>.Success(new DepartmentName(name));
    }
}
