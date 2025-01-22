using System.Text;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Models.Teachers.ValueObjects;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Models.Users.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.RegisterAsTeacher;

public sealed record RegisterAsTeacherRequest(
    string Name,
    string Surname,
    string? Patronymic,
    string Email,
    string TeacherId
) : ICommand<Teacher>;

public sealed class RegisterAsTeacherCommandHandler
    : ICommandHandler<RegisterAsTeacherRequest, Teacher>
{
    private readonly IUsersRepository _users;
    private readonly ITeachersRepository _teachers;
    private readonly IPasswordGenerator _generator;
    private readonly IPasswordHasher _hasher;
    private readonly IMailingService _mailing;

    public RegisterAsTeacherCommandHandler(
        IUsersRepository users,
        ITeachersRepository teachers,
        IPasswordGenerator generator,
        IPasswordHasher hasher,
        IMailingService mailing
    )
    {
        _users = users;
        _teachers = teachers;
        _generator = generator;
        _hasher = hasher;
        _mailing = mailing;
    }

    public async Task<Result<Teacher>> Handle(
        RegisterAsTeacherRequest command,
        CancellationToken ct = default
    )
    {
        Teacher? teacher = await _teachers.GetById(command.TeacherId.ToUpper(), ct);
        if (teacher == null)
            return TeacherErrors.NotFound();
        if (await _users.HasWithEmail(command.Email, ct))
            return UserErrors.EmailDublicate(command.Email);
        string generatedPassword = _generator.Generate();
        string hashedPassword = _hasher.Hash(generatedPassword);
        Result<User> user = User.Create(
            command.Name,
            command.Surname,
            command.Patronymic,
            command.Email,
            UserRole.Teacher.Role,
            hashedPassword
        );
        if (user.IsFailure)
            return user.Error;
        await _users.Insert(user, ct);
        Result<UserId> id = teacher.SetUserId(user);
        if (id.IsFailure)
            return id.Error;
        await _teachers.Update(teacher, ct);
        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine($"Почта: {user.Value.Email.Email}");
        messageBuilder.AppendLine($"Пароль: {generatedPassword}");
        MailingMessage message = new UserRegistrationMessage(
            user.Value.Email.Email,
            messageBuilder.ToString()
        );
        var sending = _mailing.SendMessage(message);
        return teacher;
    }
}
