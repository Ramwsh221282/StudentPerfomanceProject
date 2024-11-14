namespace SPerfomance.Domain.Abstractions;

// определение абстрактной сущности
public abstract class DomainEntity
{
    // ИД сущности
    public Guid Id { get; init; }

    public int EntityNumber { get; private set; }

    // конструктор создания объекта абстрактной сущности
    public DomainEntity(Guid id) => Id = id;

    public void SetNumber(int number) => EntityNumber = number;

    public IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        DomainValueObject valueObject = (DomainValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Aggregate(
                default(int),
                (hashcode, value) => HashCode.Combine(hashcode, value.GetHashCode())
            );

    public static bool operator ==(DomainEntity? left, DomainEntity? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(DomainEntity? left, DomainEntity? right) => !(left == right);
}
