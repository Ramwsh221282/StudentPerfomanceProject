using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects.JobTitles;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Validators;

internal sealed class JobTitleValidator(JobTitle jobTitle) : Validator<JobTitle>
{
	private readonly JobTitle[] _jobTitles = [
		new Assistant(),
		new AssociateProfessor(),
		new Professor(),
		new SeniorLecturer()
	];
	private readonly JobTitle _jobTitle = jobTitle;
	public override bool Validate()
	{
		if (_jobTitle == null)
			error.AppendError(new JobTitleNullError());
		if (_jobTitle != null && string.IsNullOrWhiteSpace(_jobTitle.Value))
			error.AppendError(new JobTitleNullError());
		if (_jobTitle != null && !string.IsNullOrWhiteSpace(_jobTitle.Value) &&
			_jobTitles.Any(job => job.Value == _jobTitle.Value) == false)
			error.AppendError(new JobTitleTypeError());
		return HasError;
	}
}
