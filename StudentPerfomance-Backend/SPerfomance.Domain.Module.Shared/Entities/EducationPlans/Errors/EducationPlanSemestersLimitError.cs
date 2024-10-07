using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

public sealed class EducationPlanSemestersLimitError : Error
{
	public EducationPlanSemestersLimitError(EducationPlan plan)
	{
		StringBuilder messageBuilder = new StringBuilder();
		messageBuilder.AppendLine($"Учебный план:");
		messageBuilder.AppendLine($"Года набора: {plan.Year}");
		messageBuilder.AppendLine($"Направления подготовки: ");
		messageBuilder.AppendLine($"{plan.Direction.Name.Name}");
		messageBuilder.AppendLine($"{plan.Direction.Code.Code}");
		messageBuilder.AppendLine($"Достиг лимита количества семестров для направления {plan.Direction.Type.Type}.");
	}
}
