using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.UpdateStudent;

public class UpdateStudentCommandHandler(
    IStudentsRepository students,
    IStudentGroupsRepository groups
) : ICommandHandler<UpdateStudentCommand, Student>
{
    public async Task<Result<Student>> Handle(
        UpdateStudentCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Student == null)
            return Result<Student>.Failure(StudentErrors.NotFound());

        var updatedStudent = command.Student.ChangeName(
            command.NewName,
            command.NewSurname,
            command.NewPatronymic
        );
        if (updatedStudent.IsFailure)
            return updatedStudent;

        updatedStudent = updatedStudent.Value.ChangeState(command.NewState);
        if (updatedStudent.IsFailure)
            return updatedStudent;

        updatedStudent = updatedStudent.Value.ChangeRecordBook(command.NewRecordBook);
        if (updatedStudent.IsFailure)
            return updatedStudent;

        var isGroupChanged = false;
        if (updatedStudent.Value.AttachedGroup.Name.Name != command.NewGroup)
        {
            var newGroup = await groups.Get(command.NewGroup, ct);
            if (newGroup == null)
                return Result<Student>.Failure(StudentGroupErrors.NotFound());
            updatedStudent.Value.ChangeGroup(newGroup);
            isGroupChanged = true;
        }

        switch (isGroupChanged)
        {
            case true:
                await students.UpdateWithGroupId(updatedStudent.Value, ct);
                break;
            case false:
                await students.Update(updatedStudent.Value, ct);
                break;
        }

        return updatedStudent;
    }
}
