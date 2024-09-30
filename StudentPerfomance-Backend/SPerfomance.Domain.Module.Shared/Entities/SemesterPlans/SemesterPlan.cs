using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

public sealed class SemesterPlan : Entity
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
	public Semester LinkedSemester { get; } = null!;
	public Discipline LinkedDiscipline { get; } = null!;

	public static CSharpFunctionalExtensions.Result<SemesterPlan> Create(Semester semester, Discipline discipline)
	{
		if (semester == null || discipline == null)
			return CSharpFunctionalExtensions.Result.Failure<SemesterPlan>(new SemesterPlanError().ToString());
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

	public override string ToString() => PlanName;
}

public static class SemesterPlanExtensions
{
	public static bool HasTeacher(this SemesterPlan plan)
	{
		if (plan == null) return false;
		if (plan.LinkedDiscipline == null) return false;
		return plan.LinkedDiscipline.Teacher != null;
	}
}
