using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Repository.Expressions;

internal sealed class Filter(SemestersRepositoryObject semester) : IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester>
{
	private readonly SemestersRepositoryObject _semester = semester;

	public Expression<Func<Domain.Module.Shared.Entities.Semesters.Semester, bool>> Build() =>
		(Domain.Module.Shared.Entities.Semesters.Semester entity) =>
			entity.Number.Value == _semester.Number;
}
