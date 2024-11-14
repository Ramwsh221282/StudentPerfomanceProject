using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.Semesters.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.CreateDiscipline.Commands;

public class CreateDisciplineCommandHandler(ISemesterPlansRepository repository)
    : ICommandHandler<CreateDisciplineCommand, SemesterPlan>
{
    private readonly ISemesterPlansRepository _repository = repository;

    public async Task<Result<SemesterPlan>> Handle(CreateDisciplineCommand command)
    {
        if (command.Semester == null)
            return Result<SemesterPlan>.Failure(SemesterErrors.NotFound());

        Result<SemesterPlan> discipline = command.Semester.AddDiscipline(command.DisciplineName);
        if (discipline.IsFailure)
            return discipline;

        await _repository.Insert(discipline.Value);
        return Result<SemesterPlan>.Success(discipline.Value);
    }
}
