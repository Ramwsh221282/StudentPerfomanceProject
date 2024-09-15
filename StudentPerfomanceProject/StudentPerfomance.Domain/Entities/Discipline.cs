using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Discipline;

namespace StudentPerfomance.Domain.Entities;

public class Discipline : Entity
{
    private Discipline(): base(Guid.Empty) { }

    private Discipline(Guid id, string name) : base(id) => Name = name;    
    
    public Teacher? Teacher { get; private set; }

    public string Name { get; private set; }

    public void ChangeName(string name)
    {
        Validator<Discipline> validator = new DisciplineValidator(name);
        if (!validator.Validate())
            return;
        Name = name;
    }

    public void SetTeacher(Teacher teacher) => Teacher = teacher;     

    public void RemoveTeacher(Teacher teacher)
    {
        if (Teacher != null && Teacher.Id == teacher.Id)
            Teacher = null;        
    }   

    public static Result<Discipline> Create(Guid id, string name)
    {        
        Validator<Discipline> validator = new DisciplineValidator(name);
        if (!validator.Validate())
            return Result.Failure<Discipline>(validator.GetErrorText());
        return new Discipline(id, name);
    }
}
