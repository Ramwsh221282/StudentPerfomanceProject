namespace SPerfomance.Domain.Models.StudentGroups.Abstractions;

public interface IStudentGroupsRepository
{
    public Task Insert(StudentGroup entity, CancellationToken ct = default);

    public Task Remove(StudentGroup entity, CancellationToken ct = default);

    public Task Update(StudentGroup entity, CancellationToken ct = default);

    public Task AttachEducationPlanId(StudentGroup group, CancellationToken ct = default);

    public Task DeattachEducationPlanId(StudentGroup group, CancellationToken ct = default);

    public Task SetNextSemester(StudentGroup group, CancellationToken ct = default);

    public Task UpdateMerge(
        StudentGroup target,
        StudentGroup merged,
        CancellationToken ct = default
    );

    public Task<StudentGroup?> Get(string name, CancellationToken ct = default);

    public Task<IReadOnlyCollection<StudentGroup>> Filter(
        string? name,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<StudentGroup>> FilterPaged(
        string? name,
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<StudentGroup>> GetAll(CancellationToken ct = default);

    public Task<IReadOnlyCollection<StudentGroup>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<bool> HasWithName(string name, CancellationToken ct = default);

    public Task<int> Count(CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);
}
