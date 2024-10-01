using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Semesters;

public record SemesterSchema : EntitySchema
{
	public byte Number { get; init; } = 0;
	public EducationPlanSchema Plan { get; init; } = new EducationPlanSchema();
	public SemesterSchema() { }
	public SemesterSchema(byte number, EducationPlanSchema? plan)
	{
		if (number > 0) Number = number;
		if (plan != null) Plan = plan;
	}
	public SemesterNumber CreateNumber() => SemesterNumber.Create(Number).Value;
	public Semester CreateDomainObject(EducationPlan plan) => Semester.Create(CreateNumber(), plan).Value;
}
