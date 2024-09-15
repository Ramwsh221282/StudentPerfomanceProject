using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Semesters;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Domain.Entities;

public class Semester : Entity
{
    private List<SemesterPlan> _contracts = [];
    public Semester(): base(Guid.Empty) {}
    private Semester(Guid id, StudentGroup group, SemesterNumber number): base(id)
    {
        Group = group;
        Number = number;
    }
    public StudentGroup Group { get; }
    public SemesterNumber Number { get; }

    public IReadOnlyCollection<SemesterPlan> Contracts => _contracts;

    public void AddContract(SemesterPlan plan)
    {
        if (plan == null || _contracts.Any(c => c.LinkedDiscipline.Name == plan.LinkedDiscipline.Name))
            return;
        _contracts.Add(plan);
    }

    public void RemoveContract(SemesterPlan plan)
    {
        SemesterPlan? target = _contracts.FirstOrDefault(c => c.LinkedDiscipline.Name == plan.LinkedDiscipline.Name);
        if (target == null)
            return;
        _contracts.Remove(target);
    }

    public static Result<Semester> Create(Guid id, StudentGroup group, SemesterNumber number)
    {
        Semester semester = new Semester(id, group, number);
        Validator<Semester> validator = new SemesterValidator(semester);
        return validator.Validate() switch
        {
            true => semester,
            false => Result.Failure<Semester>(validator.GetErrorText()),
        };
    }
}
