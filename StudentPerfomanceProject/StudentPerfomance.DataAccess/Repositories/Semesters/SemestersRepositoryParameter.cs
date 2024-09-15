namespace StudentPerfomance.DataAccess.Repositories.Semesters;

public sealed class SemestersRepositoryParameter
{
	public byte Number { get; private set; }
	public SemestersRepositoryParameter WithNumber(byte number)
	{
		Number = number;
		return this;
	}
}
