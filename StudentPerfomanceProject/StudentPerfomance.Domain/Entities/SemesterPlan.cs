using System.Text;
using CSharpFunctionalExtensions;

namespace StudentPerfomance.Domain.Entities;

public class SemesterPlan : Entity
{
	public SemesterPlan() : base(Guid.Empty) { }

	private SemesterPlan(Guid id, Semester semester, Discipline discipline) : base(id)
	{
		PlanName = CreateSemesterPlanName(semester, discipline);
		LinkedSemester = semester;
		LinkedDiscipline = discipline;
	}

	public string PlanName { get; }
	public Semester LinkedSemester { get; }
	public Discipline LinkedDiscipline { get; }

	public static Result<SemesterPlan> Create(Guid id, Semester semester, Discipline discipline)
	{
		if (semester == null || discipline == null)
			return Result.Failure<SemesterPlan>("Невозможно создать ячейку семестра");
		return new SemesterPlan(id, semester, discipline);
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
		return plan.LinkedDiscipline.Teacher != null;
	}
}
