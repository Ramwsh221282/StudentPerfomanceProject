using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Validators.EducationPlans;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Domain.Validators.Semesters;

internal class SemesterValidator(Semester semester) : Validator<Semester>
{
	private readonly Validator<SemesterNumber> _numberValidator = new SemesterNumberValidator(semester.Number);
	private readonly Validator<EducationPlan> _planValidator = new EducationPlanValidator(semester.Plan);
	private readonly Validator<Entities.StudentGroup> _groupValidator = new StudentGroupValidator(semester.Group);
	public override bool Validate()
	{
		if (!_planValidator.Validate()) _errorBuilder.AppendLine(_planValidator.GetErrorText());
		if (!_groupValidator.Validate()) _errorBuilder.AppendLine(_groupValidator.GetErrorText());
		if (!_numberValidator.Validate()) _errorBuilder.AppendLine(_numberValidator.GetErrorText());
		return _errorBuilder.Length == 0;
	}
}
