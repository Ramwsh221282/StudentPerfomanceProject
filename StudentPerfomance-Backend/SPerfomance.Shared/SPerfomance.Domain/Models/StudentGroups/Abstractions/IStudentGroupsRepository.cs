namespace SPerfomance.Domain.Models.StudentGroups.Abstractions;

public interface IStudentGroupsRepository
{
	public Task Insert(StudentGroup entity);

	public Task Remove(StudentGroup entity);

	public Task Update(StudentGroup entity);

	public Task AttachEducationPlanId(StudentGroup group);

	public Task DeattachEducationPlanId(StudentGroup group);

	public Task UpdateMerge(StudentGroup target, StudentGroup merged);

	public Task<StudentGroup?> Get(string name);

	public Task<IReadOnlyCollection<StudentGroup>> Filter(string? name);

	public Task<IReadOnlyCollection<StudentGroup>> FilterPaged(string? name, int page, int pageSize);

	public Task<IReadOnlyCollection<StudentGroup>> GetAll();

	public Task<IReadOnlyCollection<StudentGroup>> GetPaged(int page, int pageSize);

	public Task<bool> HasWithName(string name);

	public Task<int> Count();

	public Task<int> GenerateEntityNumber();
}
