namespace SPerfomance.Domain.Models.Students.Abstractions;

public interface IStudentsRepository
{
    public Task Insert(Student entity);

    public Task Remove(Student entity);

    public Task Update(Student entity);

    public Task UpdateWithGroupId(Student entity);

    public Task<bool> HasWithRecordbook(ulong recordbook);

    public Task<int> GenerateEntityNumber();
}
