using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Validators.EducationPlans;
using StudentPerfomance.Domain.Validators.StudentGroup;

namespace StudentPerfomance.Domain.Validators;

internal class StudentGroupValidator(Entities.StudentGroup group) : Validator<Entities.StudentGroup>
{
	private readonly GroupNameValidator _nameValidator = new GroupNameValidator(group.Name.Name);
	private readonly Validator<EducationPlan> _planValidator = new EducationPlanValidator(group.EducationPlan);

	public override bool Validate()
	{
		if (!_nameValidator.Validate()) _errorBuilder.AppendLine(_nameValidator.GetErrorText());
		if (!_planValidator.Validate()) _errorBuilder.AppendLine(_planValidator.GetErrorText());
		return _errorBuilder.Length == 0;
	}
}
