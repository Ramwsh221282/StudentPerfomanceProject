using System.Linq.Expressions;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Repositories.Sortings;

internal sealed class AlphabetSorting : IRepositoryComparer<StudentGroup, string>, IComparer<StudentGroup>
{
	public Expression<Func<StudentGroup, string>> BuildExpression() =>
		(StudentGroup entity) => entity.Name.Name;

	public int Compare(StudentGroup? x, StudentGroup? y)
	{
		if (x == null || y == null) return 0;
		if (string.IsNullOrWhiteSpace(x.Name.Name) || string.IsNullOrWhiteSpace(y.Name.Name)) return 0;
		return string.Compare(x.Name.Name, y.Name.Name);
	}
}
