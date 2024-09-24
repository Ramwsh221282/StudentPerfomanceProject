using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;
using StudentPerfomance.Domain.ValueObjects.EducationPlans;

namespace StudentPerfomance.Domain.Validators.EducationPlans;

internal sealed class EducationPlanValidator(EducationPlan plan) : Validator<EducationPlan>
{
	private readonly EducationPlan _plan = plan;
	private readonly Validator<YearOfCreation> _yearValidator = new YearOfCreationValidator(plan.Year);
	public override bool Validate()
	{
		if (_plan.Direction == null)
			_errorBuilder.AppendLine("Не задано направление подготовки");
		if (_plan.Direction != null && (_plan.Direction.Name == DirectionName.CreateDefault() || _plan.Direction.Code == DirectionCode.CreateDefault()))
			_errorBuilder.AppendLine("Некорректный код или имя семестра");
		if (!_yearValidator.Validate())
			_errorBuilder.AppendLine(_yearValidator.GetErrorText());
		if (_plan.Direction != null && _plan.Direction.Type.Type == DirectionTypeConstraints.BachelorType &&
		_plan.Semesters.Count > DirectionTypeConstraints.BachelorSemestersLimit)
			_errorBuilder.AppendLine($"Невозможно создать учебный план бакалавров с превышением допустимого для них количества семестров. Максимум {DirectionTypeConstraints.BachelorSemestersLimit}");
		if (_plan.Direction != null && _plan.Direction.Type.Type == DirectionTypeConstraints.MagisterType &&
		_plan.Semesters.Count > DirectionTypeConstraints.MagisterSemestersLimit)
			_errorBuilder.AppendLine($"Невозможно создать учебный план магистрантов с превышением допустимого для них количества семестров. Максимум {DirectionTypeConstraints.MagisterSemestersLimit}");
		return _errorBuilder.Length == 0;
	}
}
