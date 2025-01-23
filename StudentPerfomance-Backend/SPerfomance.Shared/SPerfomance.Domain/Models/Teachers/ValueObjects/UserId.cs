using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Domain.Models.Teachers.ValueObjects;

public sealed record UserId
{
    public Guid Id { get; }

    private UserId(Guid id)
    {
        Id = id;
    }

    public static UserId FromUserId(User user)
    {
        UserId id = new UserId(user.Id);
        return id;
    }

    public static UserId NewUserId() => new UserId(Guid.NewGuid());

    public static UserId ConcreteId(Guid id) => new UserId(id);
}
