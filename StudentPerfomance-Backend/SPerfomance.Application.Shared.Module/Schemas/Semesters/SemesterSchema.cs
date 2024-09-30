using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Semesters;

public record SemesterSchema : EntitySchema
{
	public byte Number { get; init; } = 0;
	public int PlanYear { get; init; } = 0;
	public SemesterSchema() { }
	public SemesterSchema(byte number, int planYear)
	{
		if (number > 0) Number = number;
		if (PlanYear > 0) PlanYear = planYear;
	}
	public SemesterNumber CreateNumber() => SemesterNumber.Create(Number).Value;
	public Semester CreateDomainObject(EducationPlan plan) => Semester.Create(CreateNumber(), plan).Value;
}
