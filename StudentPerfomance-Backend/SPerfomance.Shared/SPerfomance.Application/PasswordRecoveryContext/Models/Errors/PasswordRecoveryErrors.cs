using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.PasswordRecoveryContext.Models.Errors;

public static class PasswordRecoveryErrors
{
    public static readonly Error EmailWasEmpty = new("Почта не была указана");

    public static readonly Error TokenWasExpired =
        new("Срок заявки на восстановления закончился. Создайте новую заявку");

    public static readonly Error TokenWasNotFound =
        new("Заявка на восстановление пароля не была найдена");
}
