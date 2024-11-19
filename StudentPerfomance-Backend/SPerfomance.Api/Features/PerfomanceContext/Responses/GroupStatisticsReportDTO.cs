using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed record GroupStatisticsReportDTO
{
    public Guid Id { get; init; }
    public string DirectionCode { get; init; }
    public string DirectionType { get; init; }
    public byte Course { get; init; }
    public double Average { get; init; }
    public double Perfomance { get; init; }
    public string GroupName { get; init; }
    public DisciplinesStatisticsReportDTO[] Parts { get; init; }

    public GroupStatisticsReportDTO(GroupStatisticsReportEntity group)
    {
        Id = group.Id;
        DirectionCode = group.DirectionCode;
        DirectionType = group.DirectionType;
        Course = group.Course;
        GroupName = group.GroupName;
        Average = Math.Round(group.Average, 2);
        Perfomance = Math.Round(group.Perfomance, 2);
        Parts = InitializeDisciplinesReportArray(group.Parts);
    }

    private DisciplinesStatisticsReportDTO[] InitializeDisciplinesReportArray(
        List<DisciplinesStatisticsReportEntity> disciplines
    )
    {
        int count = disciplines.Count;
        if (count == 0)
            return Array.Empty<DisciplinesStatisticsReportDTO>();
        DisciplinesStatisticsReportDTO[] report = new DisciplinesStatisticsReportDTO[count];
        int left = 0;
        int right = count - 1;

        while (left <= right)
        {
            report[left] = new DisciplinesStatisticsReportDTO(disciplines[left]);
            left++;
            if (left <= right)
            {
                report[right] = new DisciplinesStatisticsReportDTO(disciplines[right]);
                right--;
            }
        }
        return report;
    }
}
