namespace StudentPerfomance.Domain.Validators.TeacherDepartment;

internal class TeacherDepartmentValidator : Validator<Entities.TeachersDepartment>
{
    private const int MAX_NAME_LENGTH = 100;

    private readonly string _departmentName;

    public TeacherDepartmentValidator(string departmentName) => _departmentName = departmentName;    

    public override bool Validate()
    {
        if (string.IsNullOrWhiteSpace(_departmentName) || _departmentName.Length > MAX_NAME_LENGTH)        
        {
            _errorBuilder.AppendLine("Название кафедры было пустым или выше 100 символов");
            return false;
        }
        return true;
    }
}
