using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;

public record SemesterSchema : EntitySchema
{
	public byte Number { get; init; } = 0;
	public SemesterSchema() { }
	public SemesterSchema(byte number)
	{
		if (number > 0) Number = number;
	}
	public SemesterNumber CreateNumber() => SemesterNumber.Create(Number).Value;
	public Semester CreateDomainObject(EducationPlan plan) => Semester.Create(CreateNumber(), plan).Value;
}
