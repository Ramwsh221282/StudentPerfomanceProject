using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Application.Commands.Group.UpdateStudentGroup;

internal sealed class StudentGroupSemestersCompressStrategy
(
	IRepository<Semester> repository,
	StudentGroup group,
	int availableAmount
)
{
	private readonly IRepository<Semester> _repository = repository;
	private readonly StudentGroup _group = group;
	private readonly int _availaleAmount = availableAmount;
	public async Task Process()
	{
		/*foreach (var semester in _group.Semesters)
		{
			if (semester.Number.Value > _availaleAmount)
				await _repository.Remove(semester);
		}
		List<Semester> semesters = _group
		.Semesters
		.Select(s => s)
		.Where(s => s.Number.Value <= _availaleAmount)
		.ToList();
		_group.ReplaceSemestersCollection(semesters);*/
	}
}
