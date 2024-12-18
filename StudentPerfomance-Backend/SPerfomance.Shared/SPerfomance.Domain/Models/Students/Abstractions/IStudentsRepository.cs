using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Domain.Models.Students.Abstractions;

public interface IStudentsRepository
{
    public Task Insert(Student entity, CancellationToken ct = default);

    public Task Remove(Student entity, CancellationToken ct = default);

    public Task Update(Student entity, CancellationToken ct = default);

    public Task UpdateWithGroupId(Student entity, CancellationToken ct = default);

    public Task<bool> HasWithRecordbook(ulong recordbook, CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);

    public Task ChangeStudentGroupId(
        Student student,
        StudentGroup group,
        CancellationToken ct = default
    );
}
