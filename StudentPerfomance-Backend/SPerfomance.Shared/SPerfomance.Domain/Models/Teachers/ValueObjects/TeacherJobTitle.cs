using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Teachers.ValueObjects;

public class TeacherJobTitle : DomainValueObject
{
    public string JobTitle { get; private set; }

    private static TeacherJobTitle[] _jobTitles =
    [
        new("Профессор"),
        new("Ассистент"),
        new("Старший преподаватель"),
        new("Доцент"),
    ];

    private TeacherJobTitle() => JobTitle = string.Empty;

    private TeacherJobTitle(string jobTitle) => JobTitle = jobTitle.Trim();

    internal static TeacherJobTitle Empty => new TeacherJobTitle();

    internal static Result<TeacherJobTitle> Create(string jobTitle)
    {
        if (string.IsNullOrWhiteSpace(jobTitle))
            return Result<TeacherJobTitle>.Failure(TeacherErrors.JobTitleEmpty());

        return _jobTitles.Any(t => t.JobTitle == jobTitle) == false
            ? Result<TeacherJobTitle>.Failure(TeacherErrors.JobTitleInvalid(jobTitle))
            : Result<TeacherJobTitle>.Success(new TeacherJobTitle(jobTitle));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return JobTitle;
    }
}
