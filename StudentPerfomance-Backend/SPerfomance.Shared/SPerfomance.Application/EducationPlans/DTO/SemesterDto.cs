using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Application.EducationPlans.DTO;

public sealed class SemesterDto
{
    public Guid Id { get; set; }
    public byte? Number { get; set; }
    public List<DisciplineDto> Disciplines { get; set; } = [];
}

public static class SemesterDtoExtensions
{
    public static SemesterDto MapFromDomain(this Semester semester) =>
        new()
        {
            Id = semester.Id,
            Number = semester.Number.Number,
            Disciplines = semester.Disciplines.Select(d => d.MapFromDomain()).ToList(),
        };
}
