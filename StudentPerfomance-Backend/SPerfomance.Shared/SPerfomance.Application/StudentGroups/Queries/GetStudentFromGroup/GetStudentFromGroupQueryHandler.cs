using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;

public class GetStudentFromGroupQueryHandler : IQueryHandler<GetStudentFromGroupQuery, Student>
{
    public async Task<Result<Student>> Handle(
        GetStudentFromGroupQuery command,
        CancellationToken ct = default
    )
    {
        if (command.Group == null)
            return Result<Student>.Failure(StudentGroupErrors.NotFound());

        var student = command.Group.GetStudent(
            command.Name.ValueOrEmpty(),
            command.Surname.ValueOrEmpty(),
            command.Patronymic.ValueOrEmpty(),
            command.State.ValueOrEmpty(),
            command.Recordbook.ValueOrEmpty()
        );

        return await Task.FromResult(student);
    }
}
