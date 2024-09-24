using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Application.Commands.Group.UpdateStudentGroup;

internal sealed class StudentGroupSemestersExtendStrategy
(
	IRepository<Semester> repository,
	IRepositoryExpression<Semester> byGroup,
	StudentGroup group,
	int availableAmount
)
{
	private readonly IRepository<Semester> _repository = repository;
	IRepositoryExpression<Semester> _byGroup = byGroup;
	private readonly StudentGroup _group = group;
	private readonly int _availableAmount = availableAmount;
	public async Task Process()
	{
		/*int currentCount = await _repository.CountWithExpression(_byGroup);
		Semester[] semesters = Enumerable.Range(currentCount + 1, _availableAmount - currentCount)
		.Select(x => Semester.Create(Guid.NewGuid(), _group, SemesterNumber.Create((byte)x).Value).Value)
		.ToArray();
		foreach (var semester in semesters)
		{
			await _group.AddSemester(_repository, semester);
		}*/
	}
}
