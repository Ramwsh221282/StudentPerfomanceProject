namespace SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

public interface ITeacherDepartmentsRepository
{
	public Task Insert(TeachersDepartments entity);

	public Task Remove(TeachersDepartments entity);

	public Task Update(TeachersDepartments entity);

	public Task<TeachersDepartments?> Get(string name);

	public Task<IReadOnlyCollection<TeachersDepartments>> GetPaged(int page, int pageSize);

	public Task<IReadOnlyCollection<TeachersDepartments>> GetPagedFiltered(string? name, int page, int pageSize);

	public Task<IReadOnlyCollection<TeachersDepartments>> GetAll();

	public Task<IReadOnlyCollection<TeachersDepartments>> GetFiltered(string? name);

	public Task<bool> HasWithName(string name);

	public Task<int> Count();

	public Task<int> GenerateEntityNumber();
}
