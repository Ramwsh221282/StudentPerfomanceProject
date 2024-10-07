using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

public sealed class EducationPlanSemesterDublicateError : Error
{
	public EducationPlanSemesterDublicateError(EducationPlan plan)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Учебный план:");
		messageBuilder.AppendLine($"Года набора: {plan.Year}");
		messageBuilder.AppendLine($"Направления подготовки: ");
		messageBuilder.AppendLine($"{plan.Direction.Name.Name}");
		messageBuilder.AppendLine($"{plan.Direction.Code.Code}");
		messageBuilder.AppendLine($"{plan.Direction.Type.Type}");
		messageBuilder.AppendLine("Уже содержит семестр с таким номером.");
	}
	public override string ToString() => error;
}
