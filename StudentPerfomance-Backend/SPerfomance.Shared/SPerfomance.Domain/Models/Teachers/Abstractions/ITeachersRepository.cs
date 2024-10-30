namespace SPerfomance.Domain.Models.Teachers.Abstractions;

public interface ITeachersRepository
{
	public Task Insert(Teacher entity);

	public Task Remove(Teacher entity);

	public Task Update(Teacher entity);

	public Task<int> GenerateEntityNumber();

	public Task<Teacher?> GetByName(string name, string surname, string? patronymic);

	public Task<Teacher?> Get(
		string name,
		string surname,
		string patronymic,
		string jobTitle,
		string workingState);
}
