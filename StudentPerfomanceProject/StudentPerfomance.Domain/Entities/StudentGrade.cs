using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.StudentGrade;
using StudentPerfomance.Domain.ValueObjects.StudentGrade;

namespace StudentPerfomance.Domain.Entities;

public class StudentGrade : Entity
{
    private StudentGrade(): base(Guid.Empty) { }

    private StudentGrade(Guid id, Teacher teacher, Discipline discipline, Student student, GradeValue value): base(id)
    {
        Teacher = teacher;
        Discipline = discipline;
        Student = student;
        GradeDate = DateTime.Now;
        Value = value;
    }

    public Teacher Teacher { get; }
    public Discipline Discipline { get; }
    public Student Student { get; }
    public DateTime GradeDate { get; }
    public GradeValue Value { get; private set; }        
    public void ChangeGrade(GradeValue newValue) => Value = newValue;

    public static Result<StudentGrade> Create(Guid id, Teacher teacher, Discipline discipline, Student student, GradeValue value)
    {
        StudentGrade grade = new StudentGrade(id, teacher, discipline, student, value);
        Validator<StudentGrade> validator = new StudentGradeValidator(grade);
        if (!validator.Validate())
            return Result.Failure<StudentGrade>(validator.GetErrorText());
        return grade;
    }
}
