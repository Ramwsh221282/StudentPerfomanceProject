using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.Semesters.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.CreateDiscipline;

public class CreateDisciplineCommandHandler(ISemesterPlansRepository repository)
    : ICommandHandler<CreateDisciplineCommand, SemesterPlan>
{
    public async Task<Result<SemesterPlan>> Handle(
        CreateDisciplineCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Semester == null)
            return Result<SemesterPlan>.Failure(SemesterErrors.NotFound());

        var discipline = command.Semester.AddDiscipline(command.DisciplineName);
        if (discipline.IsFailure)
            return discipline;

        await repository.Insert(discipline.Value, ct);
        return Result<SemesterPlan>.Success(discipline.Value);
    }
}
