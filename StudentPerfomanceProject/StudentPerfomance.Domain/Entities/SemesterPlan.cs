using System.Text;

using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Errors.SemesterPlans;

namespace StudentPerfomance.Domain.Entities;

public class SemesterPlan : Entity
{
	public SemesterPlan() : base(Guid.Empty)
	{
		PlanName = string.Empty;
	}

	private SemesterPlan(Guid id, Semester semester, Discipline discipline) : base(id)
	{
		PlanName = CreateSemesterPlanName(semester, discipline);
		LinkedSemester = semester;
		LinkedDiscipline = discipline;
	}

	public string PlanName { get; }
	public Semester? LinkedSemester { get; }
	public Discipline? LinkedDiscipline { get; }

	public static Result<SemesterPlan> Create(Semester semester, Discipline discipline)
	{
		if (semester == null || discipline == null)
			return Result.Failure<SemesterPlan>(new SemesterPlanError().ToString());
		return new SemesterPlan(Guid.NewGuid(), semester, discipline);
	}

	private string CreateSemesterPlanName(Semester semester, Discipline discipline)
	{
		StringBuilder nameBuilder = new StringBuilder();
		nameBuilder.Append("Семестр: ")
		.Append(semester.Number.Value)
		.Append(" Дисциплина: ")
		.Append(discipline.Name);
		return nameBuilder.ToString();
	}
}

public static class SemesterPlanExtension
{
	public static bool HasTeacher(this SemesterPlan plan)
	{
		if (plan == null) return false;
		if (plan.LinkedDiscipline == null) return false;
		return plan.LinkedDiscipline.Teacher != null;
	}
}
