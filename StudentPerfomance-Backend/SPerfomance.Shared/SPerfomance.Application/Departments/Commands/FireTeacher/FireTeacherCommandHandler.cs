using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.FireTeacher;

public class FireTeacherCommandHandler(
    ITeachersRepository repository,
    IUsersRepository usersRepository,
    IMailingService mailingService
) : ICommandHandler<FireTeacherCommand, Teacher>
{
    public async Task<Result<Teacher>> Handle(
        FireTeacherCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Teacher == null)
            return Result<Teacher>.Failure(TeacherErrors.NotFound());

        var fired = command.Teacher.Department.FireTeacher(command.Teacher);
        if (fired.IsFailure)
            return fired;

        await repository.Remove(fired.Value, ct);

        if (command.Teacher.UserId != null)
        {
            User? user = await usersRepository.GetById(
                command.Teacher.UserId.Id.ToString().ToUpper(),
                ct
            );
            if (user != null)
            {
                await usersRepository.Remove(user, ct);
                MailingMessage message = new UserRemoveMessage(user.Email.Email);
                var sending = mailingService.SendMessage(message);
            }
        }
        return fired;
    }
}
