namespace StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;

public record SemesterSchema : EntitySchema
{
	public byte Number { get; init; } = 0;
	public SemesterSchema() { }
	public SemesterSchema(byte number)
	{
		if (number > 0) Number = number;
	}
}
