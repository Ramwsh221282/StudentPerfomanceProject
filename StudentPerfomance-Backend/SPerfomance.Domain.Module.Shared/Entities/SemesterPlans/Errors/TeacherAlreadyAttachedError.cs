using System.Text;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class TeacherAlreadyAttachedError : Error
{
	public TeacherAlreadyAttachedError(SemesterPlan plan)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Для дисциплины {plan.Discipline}");
		messageBuilder.AppendLine($"Семестра: {plan.Semester.Number.Value}");
		messageBuilder.AppendLine($"Года набора: {plan.Semester.Plan.Year}");
		messageBuilder.AppendLine($"Направления подготовки: {plan.Semester.Plan.Direction.Name} {plan.Semester.Plan.Direction.Code.Code}");
		messageBuilder.AppendLine("Уже имеется закрепленный преподаватель.");
		messageBuilder.AppendLine("Необходимо открепить текущего преподавателя, чтобы его сменить.");
		error = messageBuilder.ToString();
	}
}
