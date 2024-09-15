using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Semesters;

public sealed class HasSemesterExpression
(
	SemestersRepositoryParameter semester,
	StudentGroupsRepositoryParameter group
)
: IRepositoryExpression<Semester>
{
	private readonly SemestersRepositoryParameter _semester = semester;
	private readonly StudentGroupsRepositoryParameter _group = group;

	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			entity.Group.Name.Name == _group.Name &&
			entity.Number.Value == _semester.Number;
}
